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
	/// Summary description for factory.
	/// </summary>
	public class factory : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Subject;
		protected System.Web.UI.WebControls.Label Description;
		protected System.Web.UI.WebControls.Label Expiration;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Button ChangeButton;
		protected System.Web.UI.WebControls.Button ListButton;
		protected System.Web.UI.WebControls.Button CreateButton;
		protected System.Web.UI.WebControls.Table Table2;
		protected System.Web.UI.WebControls.Button RemovelButton;
		protected System.Web.UI.WebControls.Button RefreshButton;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label Message;
		protected System.Web.UI.WebControls.Label Title;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Application.Lock();
			factoryPropertiesType fp = (factoryPropertiesType)Session["Factory"];
			Application.UnLock();

			// Test for expired browser session.
			if (fp == null)
				Server.Transfer("default.aspx");

			Title.Text = "Factory " + Server.HtmlEncode(fp.Name);
			Subject.Text = Server.HtmlEncode(fp.Subject == "" ? "<none supplied>" : fp.Subject);
			Description.Text = Server.HtmlEncode(fp.Description == "" ? "<none supplied>" : fp.Description);
			Expiration.Text = fp.Expiration;

			if (ConfigurationSettings.AppSettings["EnableListInstances"] == "false")
				ListButton.Visible = false;

			if (!IsPostBack)
			{
				PopulateTable();
			}
			else
			{
				foreach (string id in Request.Form.AllKeys)
				{
					if (id.StartsWith("SI"))
					{
						Application.Lock();
						ArrayList list = (ArrayList)Session["SavedInstances"];
						Session["DetailKey"] = ((ServiceInstance)list[int.Parse(id.Substring(2))]).key;
						Session["DetailBack"] = "factory.aspx";
						Application.UnLock();
						Server.Transfer("detail.aspx");
					}
				}
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
			this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
			this.ListButton.Click += new System.EventHandler(this.ListButton_Click);
			this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			this.RemovelButton.Click += new System.EventHandler(this.RemovelButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void PopulateTable()
		{
			Application.Lock();

			ArrayList instances = new ArrayList();
			Session["SavedInstances"] = instances;
			foreach (string key in (ArrayList)Session["Instances"])
			{
				instances.Add(Global.Services[key]);
			}

			instances.Sort();

			int ord = 0;
			foreach (ServiceInstance si in instances)
			{
				TableRow row = new TableRow();
				Color color = ((ord % 2) == 0) ? Color.FromArgb(0xd0, 0xd0, 0xd0) : Color.FromArgb(0xb0, 0xb0, 0xb0);

				TableCell padding = new TableCell();
				padding.Width = 10;
				row.Cells.Add(padding);

				TableCell name = new TableCell();
				name.Text = Server.HtmlEncode(si.name == "" ? "<none supplied>" : si.name) + "&nbsp;&nbsp;";
				name.BackColor = color;
				row.Cells.Add(name);

				TableCell subject = new TableCell();
				subject.Text = Server.HtmlEncode(si.subject == "" ? "<none supplied>" : si.subject) + "&nbsp;&nbsp;";
				subject.BackColor = color;
				row.Cells.Add(subject);

				TableCell state = new TableCell();
				state.Text = si.state + "&nbsp;&nbsp;";
				state.BackColor = color;
				row.Cells.Add(state);

				TableCell check = new TableCell();
				System.Web.UI.WebControls.CheckBox checkbox = new CheckBox();
				checkbox.ID = "CB" + ord.ToString();
				checkbox.Visible = state.Text.StartsWith("closed");
				check.Controls.Add(checkbox);
				check.BackColor = color;
				row.Cells.Add(check);

				TableCell link = new TableCell();
				System.Web.UI.WebControls.Button button = new Button();
				button.Text = "Details";
				button.ID = "SI" + ord.ToString();
				link.Controls.Add(button);
				link.BackColor = color;
				row.Cells.Add(link);

				Table2.Rows.Add(row);
				ord++;
			}

			if (instances.Count == 0)
			{
				Panel1.Visible = false;
				Message.Visible = true;
				Message.Text = "&nbsp;&nbsp;&nbsp;You do not have any instances.";
			}
			else
			{
				Panel1.Visible = true;
				Message.Visible = false;
			}

			Application.UnLock();
		}

		private void CreateButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("create.aspx");
		}

		private void ListButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("list.aspx");
		}

		private void ChangeButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("default.aspx");
		}

		private void RefreshButton_Click(object sender, System.EventArgs e)
		{
			PopulateTable();
		}

		private void RemovelButton_Click(object sender, System.EventArgs e)
		{
			Application.Lock();
			ArrayList saved = (ArrayList)Session["SavedInstances"];
			ArrayList instances = (ArrayList)Session["Instances"];

			for (int i = saved.Count - 1; i >= 0; i--)
			{
				string cb = Request.Form.Get("CB" + i.ToString());
				if (cb == "on")
					instances.Remove(((ServiceInstance)saved[i]).key);
			}

			PopulateTable();
			Application.UnLock();
		}
	}
}
