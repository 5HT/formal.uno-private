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
using System.Xml;
using System.Xml.Schema;
using ASAPTypes;

namespace ASAPClient
{
	/// <summary>
	/// Summary description for _default.
	/// </summary>
	public class _default : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Table Table1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (IsPostBack)
			{
				Request r = new Request();
				r.ReceiverKey = TextBox1.Text;
				r.SenderKey = ConfigurationSettings.AppSettings["hostURL"] + "/ASAPClient/ObserverService.asmx";
				r.ResponseRequired = YesNoIfError.Yes;
				r.RequestID = "id";

				string url = null;

				foreach (string id in Request.Form.AllKeys)
				{
					if (id.StartsWith("SI"))
					{
						int ord = int.Parse(id.Substring(2));
						string s = ConfigurationSettings.AppSettings["FactoryList"];
						string[] list = s.Split(' ');
						url = ConfigurationSettings.AppSettings[list[ord]].Split('|')[1];
					}
					else if (id == "Button1")
					{
						url = TextBox1.Text;
						if (url == "")
						{
							Label1.Text = "Please enter an URL or use one of the services provided above.";
							Label1.Visible = true;
							PopulateTable();
							return;
						}
					}
				}

				AsapFactoryBinding f = new AsapFactoryBinding();
				f.Url = url;
				f.RequestValue = r;

				try
				{
					factoryPropertiesType fp = f.GetProperties(new GetPropertiesRq());
					Session["Factory"] = fp;
					Session["ContextSchema"] = CompileSchema(fp.ContextDataSchema, "ContextData");
					Session["ResultSchema"] = CompileSchema(fp.ResultDataSchema, "ResultData");
				}
				catch (Exception excpt)
				{
					Label1.Text = excpt.Message;
					Label1.Visible = true;
					PopulateTable();
					return;
				}

				Server.Transfer("factory.aspx");
			}
			else
			{
				PopulateTable();
			}
		}

		private void PopulateTable()
		{
			string s = ConfigurationSettings.AppSettings["FactoryList"];
			string[] list = s.Split(' ');
			int ord = 0;
			foreach (string factory in list)
			{
				string[] x = ConfigurationSettings.AppSettings[factory].Split('|');

				TableRow row = new TableRow();
				Color color = ((ord % 2) == 0) ? Color.FromArgb(0xd0, 0xd0, 0xd0) : Color.FromArgb(0xb0, 0xb0, 0xb0);

				TableCell padding = new TableCell();
				padding.Width = 10;
				row.Cells.Add(padding);

				TableCell name = new TableCell();
				name.Text = Server.HtmlEncode(x[0]) + "&nbsp;&nbsp;";
				name.BackColor = color;
				row.Cells.Add(name);

				TableCell url = new TableCell();
				url.Text = Server.HtmlEncode(x[1]) + "&nbsp;&nbsp;";
				url.BackColor = color;
				row.Cells.Add(url);

				TableCell link = new TableCell();
				System.Web.UI.WebControls.Button button = new Button();
				button.Text = "Go";
				button.ID = "SI" + ord.ToString();
				link.Controls.Add(button);
				link.BackColor = color;
				row.Cells.Add(link);

				Table1.Rows.Add(row);
				ord++;
			}
		}

		private Object CompileSchema(schemaType st, string name)
		{
			XmlReader reader = new XmlNodeReader(st.Any);
			XmlSchema schema = XmlSchema.Read(reader, null);

			//XmlSchemaElement element = new XmlSchemaElement();
			//schema.Items.Add(element);
			//element.Name = name;
			//element.SchemaTypeName = new XmlQualifiedName(name, schema.TargetNamespace);
			
			schema.Compile(null);
			return schema;
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
