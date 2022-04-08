using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using ASAPTypes;

namespace ASAPServer
{
	/// <summary>
	/// Summary description for InstanceService.
	/// </summary>
	[System.Web.Services.WebServiceBindingAttribute(Name="AsapInstanceBinding", Namespace="http://www.oasis-open.org/asap/0.9/asap.wsdl")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(instancePropertiesType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(string[]))]
	[WebService(Namespace="http://codingcoach.com/webservices/")]
	public class InstanceService : System.Web.Services.WebService
	{
		public InstanceService()
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
	
		private ServiceInstance GenerateResponseValue()
		{
			ResponseValue = new Response();
			ResponseValue.RequestID = RequestValue.RequestID;
			ResponseValue.SenderKey = RequestValue.ReceiverKey;
			ResponseValue.ReceiverKey = RequestValue.SenderKey;

			Application.Lock();
			ServiceInstance s = (ServiceInstance) Global.Services[RequestValue.ReceiverKey];
			Application.UnLock();
			return s;
		}

		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/instance/GetProperties", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("GetPropertiesRs", typeof(instancePropertiesType), Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public instancePropertiesType GetProperties([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] GetPropertiesRq GetPropertiesRq)
		{
			ServiceInstance s = GenerateResponseValue();

			// How to handle error conditions???
			if (s == null)
				return new instancePropertiesType();

			instancePropertiesType rs = new instancePropertiesType();

			lock(s)
			{
				rs.Key = s.InstanceKey;
				rs.State = s.state;
				rs.Name = s.Name;
				rs.Subject = s.Subject;
				rs.Description = s.Description;
				rs.FactoryKey = FactoryService.FactoryKey;
				rs.Observers = (string[]) s.Observers.ToArray(typeof(string));
				rs.ContextData = s.ContextData;
				rs.ResultData = s.ResultData;
				rs.History = (historyTypeEvent[]) s.History.ToArray(typeof(historyTypeEvent));
			}

			return rs;
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/instance/SetProperties", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("SetPropertiesRs", Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public SetPropertiesRs SetProperties([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] SetPropertiesRq SetPropertiesRq)
		{
			ServiceInstance s = GenerateResponseValue();

			// How to handle error conditions???
			if (s == null)
				return new SetPropertiesRs();

			instancePropertiesType rs = new instancePropertiesType();

			lock(s)
			{
				s.Description = SetPropertiesRq.Description;
				s.Subject = SetPropertiesRq.Subject;
				s.Priority = SetPropertiesRq.Priority;
				s.ContextData = (ContextDataType)SetPropertiesRq.Data;
				s.AddHistory(historyTypeEventEventType.PropertiesSet, s.state, RequestValue.SenderKey);

				rs.Key = s.InstanceKey;
				rs.State = s.state;
				rs.Name = s.Name;
				rs.Subject = s.Subject;
				rs.Description = s.Description;
				rs.FactoryKey = FactoryService.FactoryKey;
				rs.Observers = (string[]) s.Observers.ToArray(typeof(string));
				rs.ContextData = s.ContextData;
				rs.ResultData = s.ResultData;
				rs.History = (historyTypeEvent[]) s.History.ToArray(typeof(historyTypeEvent));
			}

			SetPropertiesRs r = new SetPropertiesRs();
			r.Item = rs;
			return r;
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/instance/Subscribe", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("SubscribeRs", Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public SubscribeRs Subscribe([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] SubscribeRq SubscribeRq)
		{
			ServiceInstance s = GenerateResponseValue();

			// How to handle error conditions???
			if (s == null)
				return new SubscribeRs();

			lock(s)
			{
				if (!s.Observers.Contains(SubscribeRq.ObserverKey))
				{
					s.Observers.Add(SubscribeRq.ObserverKey);
					s.AddHistory(historyTypeEventEventType.Subscribed, s.state, RequestValue.SenderKey);
				}
			}

			return new SubscribeRs();
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/instance/Unsubscribe", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("UnsubscribeRs", Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public UnsubscribeRs Unsubscribe([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] UnsubscribeRq UnsubscribeRq)
		{
			ServiceInstance s = GenerateResponseValue();

			// How to handle error conditions???
			if (s == null)
				return new UnsubscribeRs();

			lock(s)
			{
				if (s.Observers.Contains(UnsubscribeRq.ObserverKey))
				{
					s.Observers.Remove(UnsubscribeRq.ObserverKey);
					s.AddHistory(historyTypeEventEventType.Unsubscribed, s.state, RequestValue.SenderKey);
				}
			}

			return new UnsubscribeRs();
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapHeaderAttribute("RequestValue")]
		[System.Web.Services.Protocols.SoapHeaderAttribute("ResponseValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
		[System.Web.Services.WebMethodAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.oasis-open.org/asap/0.9/asap/instance/ChangeState", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("ChangeStateRs", Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
		public ChangeStateRs ChangeState([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")] ChangeStateRq ChangeStateRq)
		{
			ServiceInstance s = GenerateResponseValue();

			// How to handle error conditions???
			if (s == null)
				return new ChangeStateRs();

			lock(s)
			{
				s.AddHistory(historyTypeEventEventType.StateChanged, ChangeStateRq.State, RequestValue.SenderKey);
			}

			ChangeStateRs rs = new ChangeStateRs();
			rs.State = ChangeStateRq.State;
			return rs;
		}
	}
}
