using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ASAPTypes;

namespace ASAPClient
{
	/// <summary>
	/// Summary description for list.
	/// </summary>
	public class list : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button BackButtn;
		protected System.Web.UI.WebControls.Button RefreshButton;
		protected System.Web.UI.WebControls.Label Title;
		protected System.Web.UI.WebControls.Table Table1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Application.Lock();
			factoryPropertiesType fp = (factoryPropertiesType)Session["Factory"];
			Application.UnLock();

			// Test for expired browser session.
			if (fp == null)
				Server.Transfer("default.aspx");

			Title.Text = "Instances of Factory " + Server.HtmlEncode(fp.Name);
			if (!IsPostBack)
			{
				PopulateTable(fp);
			}
			else
			{
				foreach (string id in Request.Form.AllKeys)
				{
					if (id.StartsWith("SI"))
					{
						Application.Lock();
						Instance[] list = (Instance[])Session["InstanceList"];
						Instance instance = list[int.Parse(id.Substring(2))];
						Session["DetailKey"] = instance.InstanceKey;
						Session["DetailBack"] = "list.aspx";
						Application.UnLock();
						Server.Transfer("detail.aspx");
					}
				}
			}
		}

		private class InstanceComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				Instance a = (Instance)x;
				Instance b = (Instance)y;
				// TODO:  Add InstanceComparer.Compare implementation
				int rc = a.Name.CompareTo(b.Name);
				if (rc == 0)
					rc = a.Subject.CompareTo(b.Subject);
				return rc;
			}
		}

		private void PopulateTable(factoryPropertiesType fp)
		{
			ListInstancesRq rq = new ListInstancesRq();
			rq.Filter = new FilterType();

			Request r = new Request();
			r.ReceiverKey = fp.Key;
			r.SenderKey = ConfigurationSettings.AppSettings["hostURL"] + "/ASAPClient/ObserverService.asmx";
			r.ResponseRequired = YesNoIfError.Yes;
			r.RequestID = "id";

			AsapFactoryBinding factory = new AsapFactoryBinding();
			factory.Url = r.ReceiverKey;
			factory.RequestValue = r;
			Instance[] rs = factory.ListInstances(rq);
			Session["InstanceList"] = rs;
			Array.Sort(rs, new InstanceComparer());

			int ord = 0;
			foreach (Instance si in rs)
			{
				TableRow row = new TableRow();
				Color color = ((ord % 2) == 0) ? Color.FromArgb(0xd0, 0xd0, 0xd0) : Color.FromArgb(0xb0, 0xb0, 0xb0);

				TableCell padding = new TableCell();
				padding.Width = 10;
				row.Cells.Add(padding);

				TableCell name = new TableCell();
				name.Text = Server.HtmlEncode(si.Name == "" ? "<none supplied>" : si.Name) + "&nbsp;&nbsp;";
				name.BackColor = color;
				row.Cells.Add(name);

				TableCell subject = new TableCell();
				subject.Text = Server.HtmlEncode(si.Subject == "" ? "<none supplied>" : si.Subject) + "&nbsp;&nbsp;";
				subject.BackColor = color;
				row.Cells.Add(subject);

				TableCell key = new TableCell();
				key.Text = Server.HtmlEncode(si.InstanceKey) + "&nbsp;&nbsp;";
				key.BackColor = color;
				row.Cells.Add(key);

				TableCell link = new TableCell();
				System.Web.UI.WebControls.Button button = new Button();
				button.Text = "Details";
				link.BackColor = color;
				link.Controls.Add(button);
				button.ID = "SI" + ord.ToString();
				row.Cells.Add(link);

				Table1.Rows.Add(row);
				ord++;
			}
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
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			this.BackButtn.Click += new System.EventHandler(this.BackButtn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BackButtn_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("factory.aspx");
		}

		private void RefreshButton_Click(object sender, System.EventArgs e)
		{
			Application.Lock();
			factoryPropertiesType fp = (factoryPropertiesType)Session["Factory"];
			Application.UnLock();

			// Test for expired browser session.
			if (fp == null)
				Server.Transfer("default.aspx");

			PopulateTable(fp);
		}
	}
}
