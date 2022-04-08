using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Timers;
using System.Configuration;
using ASAPTypes;

namespace ASAPServer 
{
	public class ServiceInstance
	{
		public string InstanceKey;
		public string Name;
		public string Subject;
		public string Description;
		public int Priority;
		public ArrayList /*of string*/ Observers;
		public ArrayList /*of historyTypeEvent*/ History;
		public ContextDataType ContextData;
		public ResultDataType ResultData;
		public stateType state;

		// When this timer object goes off, we transition the state to completed.
		public Timer timer;
		void TickEvent(Object obj, ElapsedEventArgs ea)
		{
			lock(this)
			{
				ResultData = new ResultDataType();
				ResultData.Sum = ContextData.X + ContextData.Y;
				Completed(ResultData);
			}
		}

		public ServiceInstance(int seconds)
		{
			// Set it up so this instance "completes" in one minute.
			timer = new Timer();
			timer.Interval = seconds * 1000;
			timer.Enabled = false;
			timer.AutoReset = false;
			timer.Elapsed += new ElapsedEventHandler(TickEvent);

			InstanceKey = ConfigurationSettings.AppSettings["hostUrl"] + "/ASAPServer/InstanceService.asmx?id=" + Global.MakeUniqueKey();
			Observers = new ArrayList();
			History = new ArrayList();
			state = stateType.opennotrunning;
		}

		// Caller must hold lock on instance.
		public void AddHistory(historyTypeEventEventType het, stateType newstate, string sourcekey)
		{
			historyTypeEvent h = new historyTypeEvent();
			h.EventType = het;
			h.NewState = newstate;
			h.SourceKey = sourcekey;
			h.Time = DateTime.Now;

			bool stateChanged = (state != newstate);
			h.OldState = state;

			if (state == stateType.opennotrunning && newstate == stateType.openrunning)
				timer.Start();
			
			if (stateChanged || het != historyTypeEventEventType.StateChanged)
			{
				History.Add(h);
				state = newstate;
			}

			// Inform observers when the state changes.
			if (stateChanged && state != stateType.closedcompleted)
			{
				foreach (string key in Observers)
				{
					StateChangedRq rq = new StateChangedRq();
					rq.PreviousState = h.OldState;
					rq.State = newstate;

					Request r = new Request();
					r.ReceiverKey = key;
					r.SenderKey = InstanceKey;
					r.ResponseRequired = YesNoIfError.Yes;
					r.RequestID = "id";

					ASAPClient.AsapObserverBinding observer = new ASAPClient.AsapObserverBinding();
					observer.Url = key;
					observer.RequestValue = r;
					observer.StateChanged(rq);
				}
			}
		}

		public void Completed(ResultDataType data)
		{
			AddHistory(historyTypeEventEventType.StateChanged, stateType.closedcompleted, InstanceKey);

			foreach (string key in Observers)
			{
				CompletedRq rq = new CompletedRq();
				rq.InstanceKey = InstanceKey;
				rq.ResultData = data;

				Request r = new Request();
				r.ReceiverKey = key;
				r.SenderKey = InstanceKey;
				r.ResponseRequired = YesNoIfError.Yes;
				r.RequestID = "id";

				ASAPClient.AsapObserverBinding observer = new ASAPClient.AsapObserverBinding();
				observer.Url = key;
				observer.RequestValue = r;
				observer.Completed(rq);
			}
		}
	};

	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		static public Hashtable Services = new Hashtable();

		static private int keycounter = 0;
		static public string MakeUniqueKey()
		{
			string s = String.Format("{0}.{1,3:000}", DateTime.Now.Ticks, keycounter);
			keycounter = (keycounter + 1) % 1000;
			return s;
		}

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{

		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

