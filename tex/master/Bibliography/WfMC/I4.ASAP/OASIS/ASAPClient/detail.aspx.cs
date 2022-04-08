using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Schema;
using ASAPTypes;

namespace ASAPClient
{
	/// <summary>
	/// Summary description for detail.
	/// </summary>
	public class detail : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Title;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Name;
		protected System.Web.UI.WebControls.Label Subject;
		protected System.Web.UI.WebControls.Label Description;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label State;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Factory;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Button RefreshButton;
		protected System.Web.UI.WebControls.Button BackButtn;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Button StartButton;
		protected System.Web.UI.WebControls.Label RDLabel;
		protected System.Web.UI.WebControls.Table HistoryTable;
		protected System.Web.UI.WebControls.Label CD;
		protected System.Web.UI.WebControls.Label RD;
		protected System.Web.UI.WebControls.Label CDLabel;
		protected System.Web.UI.WebControls.Table CDTable;
		protected System.Web.UI.WebControls.Table RDTable;
		protected System.Web.UI.WebControls.Label Key;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Application.Lock();
			factoryPropertiesType fp = (factoryPropertiesType)Session["Factory"];
			Object rk = Session["DetailKey"];
			Application.UnLock();

			// Test for expired browser session.
			if (fp == null)
				Server.Transfer("default.aspx");

			Request r = new Request();
			r.ReceiverKey = (string)rk;
			r.SenderKey = ConfigurationSettings.AppSettings["hostURL"] + "/ASAPClient/ObserverService.asmx";
			r.ResponseRequired = YesNoIfError.Yes;
			r.RequestID = "id";

			AsapInstanceBinding instance = new AsapInstanceBinding();
			instance.Url = r.ReceiverKey;
			instance.RequestValue = r;
			instancePropertiesType ipt = instance.GetProperties(new GetPropertiesRq());

			Name.Text = Server.HtmlEncode(ipt.Name == "" ? "<none supplied>" : ipt.Name);
			Subject.Text = Server.HtmlEncode(ipt.Subject == "" ? "<none supplied>" : ipt.Subject);
			Description.Text = Server.HtmlEncode(ipt.Description == "" ? "<none supplied>" : ipt.Description);
			State.Text = ipt.State.ToString();
			Key.Text = Server.HtmlEncode(ipt.Key);
			Factory.Text = Server.HtmlEncode(ipt.FactoryKey);
			StartButton.Visible = ipt.State == stateType.opennotrunning;

			int ord = 0;
			foreach (historyTypeEvent h in ipt.History)
			{
				TableRow row = new TableRow();
				Color color = ((ord % 2) == 0) ? Color.FromArgb(0xd0, 0xd0, 0xd0) : Color.FromArgb(0xb0, 0xb0, 0xb0);

				TableCell padding = new TableCell();
				padding.Width = 10;
				row.Cells.Add(padding);

				TableCell time = new TableCell();
				time.Text = h.Time.ToString() + "&nbsp;&nbsp;";
				time.BackColor = color;
				row.Cells.Add(time);

				TableCell ev = new TableCell();
				ev.Text = h.EventType.ToString() + "&nbsp;&nbsp;";
				ev.BackColor = color;
				row.Cells.Add(ev);

				TableCell state = new TableCell();
				state.Text = h.NewState.ToString() + "&nbsp;&nbsp;";
				state.BackColor = color;
				row.Cells.Add(state);

				TableCell key = new TableCell();
				key.Text = Server.HtmlEncode(h.SourceKey);
				key.BackColor = color;
				row.Cells.Add(key);
			
				HistoryTable.Rows.Add(row);
				ord++;
			}

			// Add spacing between history table and context data header.
			{
				TableRow row = new TableRow();
				TableCell padding = new TableCell();
				padding.Text = "&nbsp;";
				row.Cells.Add(padding);
				HistoryTable.Rows.Add(row);
			}

			// Extract the context and result data.
			XmlElement cd = null;
			XmlElement rd = null;
			foreach (XmlElement elem in ipt.Data)
			{
				if (elem.LocalName == "ContextData")
					cd = elem;
				else if (elem.LocalName == "ResultData")
					rd = elem;
			}

			if (cd != null)
			{
				CD.Text = FormatData(cd, "ContextSchema");
			}
			else
			{
				CDLabel.Visible = false;
				CDTable.Visible = false;
			}

			if (rd != null)
			{
				RD.Text = FormatData(rd, "ResultSchema");
			}
			else
			{
				RDLabel.Visible = false;
				RDTable.Visible = false;
			}
		}

		private Regex re = new Regex("<((?<tag>\\w+))\\s+xmlns=\"(.*?)\"(.*?)</\\k<tag>", RegexOptions.Compiled | RegexOptions.Singleline);

		private string FormatData(XmlElement e, string schemaName)
		{
			StringBuilder sb = new StringBuilder();
			System.IO.StringWriter stream = new System.IO.StringWriter(sb);
			XmlTextWriter w = new XmlTextWriter(stream);
			w.Formatting = Formatting.Indented;
			w.Indentation = 4;
			e.WriteTo(w);

			// .NET doesn't like using namespace prefixes when formatting XML into text.  So the
			// remainder of this method uses regular expressions to extract the namespace from
			// xmlns attributes, lookup the prefix used in the schema, and insert it before the tag.

			Hashtable t = new Hashtable();
			XmlSchema schema = (XmlSchema)Session[schemaName];
			foreach (XmlQualifiedName qn in schema.Namespaces.ToArray())
			{
				t.Add(qn.Namespace, qn.Name);
			}
			t.Add("http://www.oasis-open.org/asap/0.9/asap.xsd", "asap");

			while (true)
			{
				Match m = re.Match(sb.ToString());
				if (!m.Success)
					break;

				string tag = m.Groups[1].Captures[0].ToString();
				string ns = m.Groups[2].Captures[0].ToString();
				string body = m.Groups[3].Captures[0].ToString();
				string prefix = "";
				if (t.ContainsKey(ns))
					prefix = (string)t[ns] + ":";

				sb.Remove(m.Index, m.Length);
				sb.Insert(m.Index, "<" + prefix + tag + body + "</" + prefix + tag);
			}

			return "<pre>" + Server.HtmlEncode(sb.ToString()) + "</pre>";
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.BackButtn.Click += new System.EventHandler(this.BackButtn_Click);
			this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BackButtn_Click(object sender, System.EventArgs e)
		{
			Application.Lock();
			string destination = (string)Session["DetailBack"];
			Application.UnLock();

			Server.Transfer(destination);
		}

		private void StartButton_Click(object sender, System.EventArgs e)
		{
			Request r = new Request();
			r.ReceiverKey = (string)Session["DetailKey"];
			r.SenderKey = ConfigurationSettings.AppSettings["hostURL"] + "/ASAPClient/ObserverService.asmx";
			r.ResponseRequired = YesNoIfError.Yes;
			r.RequestID = "id";

			ChangeStateRq rq = new ChangeStateRq();
			rq.State = stateType.openrunning;

			AsapInstanceBinding instance = new AsapInstanceBinding();
			instance.Url = r.ReceiverKey;
			instance.RequestValue = r;
			instance.ChangeState(rq);

			Server.Transfer("detail.aspx");
		}
	}
}
