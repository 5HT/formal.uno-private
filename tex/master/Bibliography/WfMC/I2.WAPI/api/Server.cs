
using System;
using System.Collections;

namespace ILS.Workflow {

	public class Server : MarshalByRefObject {

		// Object Context

		string engine = "1.0";
		string scope = "bravo";
		int error_code = 0;
		int error_sub_code = 0;
		string url = "http://localhost:7070/workflow";

		// Ctor

		public Server(string url, int port, string service) { }

		// Properties

		public string Engine { get { return engine; } }
		public int ErrorCode { get { return error_code; } }
		public int ErrorSubCode { get { return error_sub_code; } }
		public string Scope { get { return scope; } }

		// Methods

		public void Connect(string user, string password) { }
		public void Disconnect() { }
		public IProcessDefinition CreateProcessDefinition() { return null; }
		public void DeleteProcessDefinition(IProcessDefinition procdef) {}
		public IDictionary ListProcessDefinitions(IFilter filter) { return null; }
		public IDictionary ListProcessInstances(IFilter filter)  { return null; }
		public IProcessInstance GetProcessInstance(string proc)  { return null; }
		public IDictionary ListActivityInstances(IFilter filter)  { return null; }
		public IActivityInstance GetActivityInstance(string proc, string act)  { return null; }
		public IDictionary ListWorkItems(IFilter filter)  { return null; }
		public IWorkItem GetWorkItem(string proc, string workitem)  { return null; }
		public IApplicationDefinition CreateApplicationDefinition(string name)  { return null; }
		public void DeleteApplicationDefinition() { }
		public IParticipantDefinition CreateParticipantDefinition(string name)  { return null; }
		public void DeleteParticipantDefinition() { }

	}

}
