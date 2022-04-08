using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using ASAPTypes;

namespace ASAPClient 
{
	public class ServiceInstance : IComparable
	{
		public string key;
		public string name;
		public string subject;
		public stateType state;
		#region IComparable Members

		public int CompareTo(object obj)
		{
			ServiceInstance other = (ServiceInstance)obj;
			int rc = name.CompareTo(other.name);
			if (rc == 0)
				rc = subject.CompareTo(other.subject);
			return rc;
		}

		#endregion
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

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{

		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
			Session["Instances"] = new ArrayList();
			Session["Sequence"] = 0;
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

