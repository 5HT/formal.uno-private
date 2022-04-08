
using System.Collections;

namespace ILS.Workflow {

	// IServer

	public interface IServer {
		string Engine { get; }
		int ErrorCode { get; }
		int ErrorSubCode { get; }
		string Scope { get; }

		void Connect(string user,  string password);
		void Disconnect();
		IProcessDefinition CreateProcessDefinition();
		void DeleteProcessDefinition(IProcessDefinition procdef);
		IDictionary ListProcessDefinitions(IFilter filter);
		IDictionary ListProcessInstances(IFilter filter);
		IProcessInstance GetProcessInstance(string proc);
		IDictionary ListActivityInstances(IFilter filter);
		IActivityInstance GetActivityInstance(string proc, string act);
		IDictionary ListWorkItems(IFilter filter);
		IWorkItem GetWorkItem(string proc, string workitem);
		IApplicationDefinition CreateApplicationDefinition(string name);
		void DeleteApplicationDefinition();
		IParticipantDefinition CreateParticipantDefinition(string name);
		void DeleteParticipantDefinition();
	}

	// IFilter

	public interface IFilter {
		int Type { get; }
		int Length { get; }
		string AttributeName { get; }
		int Comparison { get; }
		string FilterString { get; }
	}

	// IActivityDefinition

	public interface IActivityDefinition {
		IDictionary Attributes { get; }
		IApplicationDefinition Application { get; }
		IProcessDefinition Process { get; }
		string Name { get; }
	}

	// IProcessDefinition

	public interface IProcessDefinition {
		IDictionary Activities { get; }
		IDictionary Application { get; }
		IDictionary Data { get; }
		string ID { get; }
		string Name { get; }
		IDictionary Participants { get; }
		IDictionary States { get; }
		IDictionary Transitions { get; }

		IProcessInstance CreateInstance();
		void ChangeInstanceState(IFilter intance, string state);
		void ChangeActivityInstanceState(string actdef, IFilter inst, string state);
		void TerminateInstances(IFilter intance);
		void AssignInstanceAttribute(IFilter intance, string name, object value);
		void AssignActivityInstanceAttribute(string act, IFilter intance, string name, object value);
		void AbortInstances(IFilter intance);
		IActivityDefinition AddActivity(string name);
		IApplicationDefinition AddApplication(string name);
        IParticipantDefinition AddParticipant(string name);
		IProcessDataDefinition AddData(string name);
		ITransitionDefinition AddTransition(string name);
	}
		
	public interface IProcessInstance {
		IDictionary Attributes { get; }
		string DataReference { get; }
		string ID { get; }
		string Name { get; }
		IDictionary Participants { get; }
		int Priority { get; }
		IProcessDefinition ProcessDefinition { get; }
		string ProcessDefinitionID { get; }
		string State { get; }
		IDictionary States { get; }
		
		IProcessInstance Start();
		void Terminate();
		void ChangeState(string state);
		void AssignAttribute(string name, object value);
		void Abort();
	}		
		
	public interface IActivityInstance {
		IDictionary Attributes { get; }
		string DataReference { get; }
		string ID { get; }
		string Name { get; }
		IDictionary Participants { get; }
		int Priority { get; }
		IProcessInstance ProcessInstance { get; }
		string ProcessInstanceID { get; }
		string State { get; }
		IDictionary States { get; }

		void ChangeState(string state);
		void AssignAttribute(string name, object value);
	}

	public interface IWorkItem {
		IActivityInstance ActivityProcessInstance { get; }
		string ActivityInstanceID { get; }
		IDictionary Attributes { get; }
		string DataReference { get; }
		string ID { get; }
		string Name { get; }
		IDictionary Participants { get; }
		int Priority { get; }
		IProcessInstance ProcessInstance { get; }
		string ProcessInstanceID { get; }

		void Complete();
		void AssignAttribute(string name, object value);
		void Reassign(string srcuser, string dstuser);
	}

	public interface ITransitionDefinition {
		IDictionary Attributes { get; }
		string ID { get; }
		string Name { get; }
		IActivityDefinition From { get; }
		IActivityDefinition To { get; }
	}

	public interface IParticipantDefinition {
		IDictionary Attributes { get; }
		string ID { get; }
		string Name { get; }
		int Type { get; }
	}

	public interface IApplicationDefinition {
		IDictionary Attributes { get; }
		string ID { get; }
		string Name { get; }
	}

	public interface IProcessDataDefinition {
		IDictionary Attributes  { get; }
		string ID  { get; }
		string Name  { get; }
		int Type  { get; }
	}

	public interface IAttribute {
		string Name  { get; }
		int DataType  { get; }
	}
}
