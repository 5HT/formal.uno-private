using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
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
	/// Summary description for create.
	/// </summary>
	public class create : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox NameBox;
		protected System.Web.UI.WebControls.TextBox SubjectBox;
		protected System.Web.UI.WebControls.TextBox DescriptionBox;
		protected System.Web.UI.WebControls.RadioButton StartYes;
		protected System.Web.UI.WebControls.RadioButton StartNo;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Button CreateButton;
		protected System.Web.UI.WebControls.Button CancelButton;
		protected System.Web.UI.WebControls.Label ErrorBox;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label CDSchema;
		protected System.Web.UI.WebControls.Table CSDTable;
		protected System.Web.UI.WebControls.Table CDTable;
		protected System.Web.UI.WebControls.TextBox CDData;
		protected System.Web.UI.WebControls.Label Title;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			System.IO.StringWriter stream = new System.IO.StringWriter(sb);
			XmlTextWriter writer = new XmlTextWriter(stream);
			writer.Formatting = Formatting.Indented;
			writer.Indentation = 4;

			Application.Lock();
			factoryPropertiesType fp = (factoryPropertiesType)Session["Factory"];
			XmlSchema schema = (XmlSchema)Session["ContextSchema"];
			if (ViewState["CreationSequence"] == null)
				ViewState["CreationSequence"] = Session["Sequence"];
			Application.UnLock();

			// Test for expired browser session.
			if (fp == null)
				Server.Transfer("default.aspx");

			schema.Write(writer);
			sb.Remove(0, sb.ToString().IndexOf("\n") + 1);
			CDSchema.Text = "<pre>" + Server.HtmlEncode(sb.ToString()) + "</pre>";

			if (!IsPostBack)
			{
				CDData.Text = "<asap:ContextData>\n";
				XmlSchemaComplexType xst = (XmlSchemaComplexType)schema.SchemaTypes[new XmlQualifiedName("ContextDataType", schema.TargetNamespace)];
				XmlSchemaSequence xsp = (XmlSchemaSequence)xst.Particle;
				if (xsp != null)
				{
					foreach (XmlSchemaObject obj in xsp.Items)
					{
						XmlSchemaElement xse = (XmlSchemaElement)obj;
						if (xse != null)
						{
							string n;
							if (xse.Name == xse.QualifiedName.ToString())
							{
								n = xse.Name;
							}
							else
							{
								string prefix = "";
								foreach (XmlQualifiedName qn in schema.Namespaces.ToArray())
								{
									if (qn.Namespace == xse.QualifiedName.Namespace)
										prefix = qn.Name + ":";
								}
								n = prefix + xse.Name;
							}
							CDData.Text += ("   <" + n + "></" + n + ">\n");
						}
					}
				}

				CDData.Text = CDData.Text + "</asap:ContextData>";
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
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void CreateButton_Click(object sender, System.EventArgs e)
		{
			Application.Lock();
			factoryPropertiesType fp = (factoryPropertiesType)Session["Factory"];
			XmlSchema schema = (XmlSchema)Session["ContextSchema"];
			int seq = (int)Session["Sequence"];
			Session["Sequence"] = seq + 1;
			Application.UnLock();

			// Catch a browser refresh done immediately after creating a new instance.
			// Don't let it create a duplicate instance.
			if (seq > (int)ViewState["CreationSequence"])
				Server.Transfer("factory.aspx");

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
			nsmgr.AddNamespace("asap", "http://www.oasis-open.org/asap/0.9/asap.xsd");
			foreach (XmlQualifiedName qn in schema.Namespaces.ToArray())
			{
				nsmgr.AddNamespace(qn.Name, qn.Namespace);
			}

			XmlParserContext pc = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
			XmlValidatingReader vr = new XmlValidatingReader(CDData.Text, XmlNodeType.Document, pc);
			vr.Schemas.Add(schema);
			vr.ValidationType = ValidationType.Schema;
			//vr.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

			XmlDocument doc = new XmlDocument();
			XmlNode n = doc.ReadNode(vr);

			CreateInstanceRq rq = new CreateInstanceRq();
			rq.StartImmediately = StartYes.Checked;
			rq.ObserverKey = ConfigurationSettings.AppSettings["hostURL"] + "/ASAPClient/ObserverService.asmx";
			rq.Name = NameBox.Text;
			rq.Subject = SubjectBox.Text;
			rq.Description = DescriptionBox.Text;
			rq.Other = new XmlElement[1] { (XmlElement)n }; // context data

			Request r = new Request();
			r.ReceiverKey = fp.Key;
			r.SenderKey = rq.ObserverKey;
			r.ResponseRequired = YesNoIfError.Yes;
			r.RequestID = "id";

			AsapFactoryBinding factory = new AsapFactoryBinding();
			factory.Url = r.ReceiverKey;
			factory.RequestValue = r;
			CreateInstanceRs rs = factory.CreateInstance(rq);

			// Note: the server might inform us the state transitioned from not running to
			// running (or even completed!) before we reach this point.  Not sure how to handle
			// this properly.

			ServiceInstance s = new ServiceInstance();
			s.state = rq.StartImmediately ? stateType.openrunning : stateType.opennotrunning;
			s.key = rs.InstanceKey;
			s.name = rq.Name;
			s.subject = rq.Subject;

			Application.Lock();
			Global.Services.Add(s.key, s);
			ArrayList instances = (ArrayList)Session["Instances"];
			instances.Add(s.key);
			Application.UnLock();

			Server.Transfer("factory.aspx");
		}

		private void CancelButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("factory.aspx");
		}
	}

	public class BadData : System.Exception
	{
	};
}
