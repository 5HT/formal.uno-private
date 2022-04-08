using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Xml;
using ASAPTypes;

namespace ASAPServer
{
	/// <summary>
	/// Summary description for Service1.
	/// </summary>
	[System.Web.Services.WebServiceBindingAttribute(Name="AsapFactoryBinding", Namespace="http://www.oasis-open.org/asap/0.9/asap.wsdl")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(factoryPropertiesType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(string[]))]
	[WebService(Namespace="http://codingcoach.com/webservices/")]
	public class FactoryService : System.Web.Services.WebService
	{
		public FactoryService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion
		
		public Request RequestValue;
        
		public Response ResponseValue;

		public static string FactoryKey = ConfigurationSettings.AppSettings["hostUrl"] + "/ASAPServer/FactoryService.asmx";

		private void GenerateResponseValue()
		{
			ResponseValue = new Response();
			ResponseValue.RequestID = RequestValue.RequestID;
			ResponseValue.SenderKey = FactoryKey;
			ResponseValue.ReceiverKey = RequestValue.SenderKey;
		}
		
		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/factory/GetProperties", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("GetPropertiesRs", typeof(factoryPropertiesType), Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public factoryPropertiesType GetProperties([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] GetPropertiesRq GetPropertiesRq)
		{
			GenerateResponseValue();
			
			factoryPropertiesType rs = new factoryPropertiesType();
			rs.Key = FactoryKey;
			rs.Name = "AsapDemo";
			rs.Subject = "Demo ASAP factory.";
			rs.Description = "demo description";
			rs.ContextDataSchema = ReadDemoSchema("ContextData");
			rs.ResultDataSchema = ReadDemoSchema("ResultData");
			rs.Expiration = "P1D";

			return rs;
		}

		private schemaType ReadDemoSchema(string name)
		{
			XmlTextReader reader = new XmlTextReader(ConfigurationSettings.AppSettings["hostUrl"] + "/ASAPServer/" + name + ".xsd");
			XmlDocument doc = new XmlDocument();
			XmlNode node = doc.ReadNode(reader);

			schemaType st = new schemaType();
			st.href = ConfigurationSettings.AppSettings["hostUrl"] + "/ASAPServer/" + name + ".xsd";
			st.Any = (XmlElement)node;
			return st;
		}
		
		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/factory/CreateInstance", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("CreateInstanceRs", Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public CreateInstanceRs CreateInstance([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] CreateInstanceRq CreateInstanceRq)
		{
			GenerateResponseValue();

			CreateInstanceRs rs = new CreateInstanceRs();
			ServiceInstance si = new ServiceInstance(CreateInstanceRq.ContextData.Seconds);
			si.Description = CreateInstanceRq.Description;
			si.Name = CreateInstanceRq.Name;
			si.Subject = CreateInstanceRq.Subject;
			si.ContextData = CreateInstanceRq.ContextData;
			si.Observers.Add(CreateInstanceRq.ObserverKey);
			si.AddHistory(historyTypeEventEventType.InstanceCreated,
						  CreateInstanceRq.StartImmediately ? stateType.openrunning : stateType.opennotrunning,
						  FactoryKey);

			Application.Lock();
			Global.Services.Add(si.InstanceKey, si);
			Application.UnLock();

			rs.InstanceKey = si.InstanceKey;
			return rs;
		}
		
		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/factory/ListInstances", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlArrayAttribute("ListInstancesRs", Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		[return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd", IsNullable=false)]
		public Instance[] ListInstances([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] ListInstancesRq ListInstanceRq)
		{
			GenerateResponseValue();

			Application.Lock();
			Instance[] list = new Instance[Global.Services.Count];
			int i = 0;

			foreach (DictionaryEntry de in Global.Services)
			{
				ServiceInstance s = (ServiceInstance)de.Value;
				Instance t = new Instance();
				list[i++] = t;

				lock(s)
				{
					t.InstanceKey = s.InstanceKey;
					t.Name = s.Name;
					t.PrioritySpecified = false;
					t.Subject = s.Subject;
				}
			}

			Application.UnLock();
			return list;
		}
	}
}
