using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Configuration;
using ASAPTypes;

namespace ASAPServer
{
	/// <summary>
	/// Summary description for ObserverService.
	/// </summary>
	[System.Web.Services.WebServiceBindingAttribute(Name="AsapObserverBinding", Namespace="http://www.oasis-open.org/asap/0.9/asap.wsdl")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(observerPropertiesType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(string[]))]
	[WebService(Namespace="http://codingcoach.com/webservices/")]
	public class ObserverService : System.Web.Services.WebService
	{
		public ObserverService()
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

		private ASAPClient.ServiceInstance GenerateResponseValue()
		{
			ResponseValue = new Response();
			ResponseValue.RequestID = RequestValue.RequestID;
			ResponseValue.SenderKey = RequestValue.ReceiverKey;
			ResponseValue.ReceiverKey = RequestValue.SenderKey;

			Application.Lock();
			ASAPClient.ServiceInstance s = (ASAPClient.ServiceInstance) ASAPClient.Global.Services[RequestValue.SenderKey];
			Application.UnLock();
			return s;
		}

		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/observer/GetProperties", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("GetPropertiesRs", typeof(observerPropertiesType), Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public observerPropertiesType GetProperties([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] object GetPropertiesRq)
		{
			GenerateResponseValue();

			observerPropertiesType opt = new observerPropertiesType();
			opt.Key = ConfigurationSettings.AppSettings["hostUrl"] + "/ASAPClient/ObserverService.asmx";
			return opt;
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/observer/Completed", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("CompletedRs", Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public CompletedRs Completed([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] CompletedRq CompletedRq)
		{
			ASAPClient.ServiceInstance s = GenerateResponseValue();

			if (s != null)
				s.state = stateType.closedcompleted;

			return new CompletedRs();
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/observer/StateChanged", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("StateChangedRs", Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public StateChangedRs StateChanged([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] StateChangedRq StateChangedRq)
		{
			ASAPClient.ServiceInstance s = GenerateResponseValue();

			if (s != null)
				s.state = StateChangedRq.State;

			return new StateChangedRs();
		}
	}
}
