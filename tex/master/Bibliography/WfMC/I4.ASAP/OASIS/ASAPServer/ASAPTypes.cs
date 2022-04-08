﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

//
// This source code was auto-generated by wsdl, Version=1.1.4322.573.
//
// And then extensively modified by hand.
//
namespace ASAPTypes {
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;

	[XmlTypeAttribute(Namespace="http://codingcoach/schema.xsd")]
	[XmlRootAttribute(Namespace="http://codingcoach/schema.xsd", IsNullable=false)]
	public class ContextDataType {

		[XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
		public int X;

		[XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
		public int Y;

		[XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
		public int Seconds;
	}

	[XmlTypeAttribute(Namespace="http://codingcoach/schema.xsd")]
	[XmlRootAttribute(Namespace="http://codingcoach/schema.xsd", IsNullable=false)]
	public class ResultDataType {

		[XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
		public int Sum;
	}

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    [XmlRootAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd", IsNullable=false)]
    public class Request : SoapHeader {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string SenderKey;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string ReceiverKey;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public YesNoIfError ResponseRequired;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string RequestID;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public enum YesNoIfError {

        Yes,

        No,

        IfError,
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class StateChangedRs {
	}

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class StateChangedRq {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public stateType State;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public stateType PreviousState;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public enum stateType {

        [XmlEnumAttribute("open.notrunning")]
        opennotrunning,

        [XmlEnumAttribute("open.notrunning.suspended")]
        opennotrunningsuspended,

        [XmlEnumAttribute("open.running")]
        openrunning,

        [XmlEnumAttribute("closed.completed")]
        closedcompleted,

        [XmlEnumAttribute("closed.abnormalCompleted")]
        closedabnormalCompleted,

        [XmlEnumAttribute("closed.abnormalCompleted.terminated")]
        closedabnormalCompletedterminated,

        [XmlEnumAttribute("closed.abnormalCompleted.aborted")]
        closedabnormalCompletedaborted,
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class CompletedRs {
	}

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class CompletedRq {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string InstanceKey;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public ResultDataType ResultData;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class Instance {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string InstanceKey;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Name;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Subject;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public int Priority;

        [XmlIgnoreAttribute()]
        public bool PrioritySpecified;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class FilterType {

        [XmlAttributeAttribute(DataType="NMTOKEN")]
        public string filterType;

        [XmlTextAttribute()]
        public string Value;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class ListInstancesRq {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public FilterType Filter;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class CreateInstanceRs {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string InstanceKey;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class CreateInstanceRq {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public bool StartImmediately;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string ObserverKey;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Name;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Subject;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Description;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public ContextDataType ContextData;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class ChangeStateRs {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public stateType State;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class ChangeStateRq {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public stateType State;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class UnsubscribeRs {
	}

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class UnsubscribeRq {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string ObserverKey;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class SubscribeRs {
	}

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class SubscribeRq {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string ObserverKey;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class SetPropertiesRs {

        public object Item;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class SetPropertiesRq {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Subject;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Description;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public int Priority;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public ContextDataType Data;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class GetPropertiesRq {
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class GetPropertiesRs {
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class observerPropertiesType : GetPropertiesRs {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string Key;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class factoryPropertiesType : GetPropertiesRs {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string Key;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Name;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Subject;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Description;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public schemaType ContextDataSchema;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public schemaType ResultDataSchema;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="duration")]
        public string Expiration;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class schemaType {

        [XmlAnyElementAttribute()]
        public XmlElement Any;

        [XmlAttributeAttribute(DataType="anyURI")]
        public string href;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class instancePropertiesType : GetPropertiesRs {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string Key;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public stateType State;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Name;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Subject;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public string Description;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string FactoryKey;

        [XmlArrayAttribute(Form=XmlSchemaForm.Qualified)]
        [XmlArrayItemAttribute("ObserverKey", Form=XmlSchemaForm.Qualified, DataType="anyURI", IsNullable=false)]
        public string[] Observers;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public ContextDataType ContextData;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public ResultDataType ResultData;

        [XmlArrayAttribute(Form=XmlSchemaForm.Qualified)]
        [XmlArrayItemAttribute("Event", Form=XmlSchemaForm.Qualified, IsNullable=false)]
        public historyTypeEvent[] History;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public class historyTypeEvent {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public DateTime Time;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public historyTypeEventEventType EventType;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string SourceKey;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public object Details;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public stateType OldState;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified)]
        public stateType NewState;
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    public enum historyTypeEventEventType {

        InstanceCreated,

        PropertiesSet,

        StateChanged,

        Subscribed,

        Unsubscribed,

        Error,
    }

    [XmlTypeAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd")]
    [XmlRootAttribute(Namespace="http://www.oasis-open.org/asap/0.9/asap.xsd", IsNullable=false)]
    public class Response : SoapHeader {

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string SenderKey;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string ReceiverKey;

        [XmlElementAttribute(Form=XmlSchemaForm.Qualified, DataType="anyURI")]
        public string RequestID;
    }
}
