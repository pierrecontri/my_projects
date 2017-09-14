#define TRACE

using System;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security.Permissions;

public partial class _Default : System.Web.UI.Page 
{
    ServerManagment sm = null;
    // filter the requested services
    String[] listFilterServices = ConfigurationSettings.AppSettings["ListOfServicesToManage"].Split(new char[] { ';' });

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SrvManagment"] == null)
        {
            sm = new ServerManagment(Request.UserHostName, this.listFilterServices);
            Session["SrvManagment"] = sm;
        }
        else
        {
            sm = (ServerManagment)Session["SrvManagment"];
        }

        GridView1.DataMember = "ServerManagment";
        GridView1.DataSource = sm.ServicesList;
    }
    
    protected void UpdateListError()
    {
        String lastErr = "";
        TextBox1.Text = "";
        do
        {
            lastErr = sm.GetLastError();
            TextBox1.Text += lastErr + "\n";
        } while (lastErr != "");
    }

    protected void ButtonRestartServices_Click(object sender, EventArgs e)
    {
        if (sm != null)
        {
            sm.StopAllServicesSelected();
            sm.StartAllServicesSelected();
        }
    }

    protected void ButtonStopServices_Click(object sender, EventArgs e)
    {
        if (sm != null)
            sm.StopAllServicesSelected();
    }

    protected void ButtonStartServices_Click(object sender, EventArgs e)
    {
        if (sm != null)
            sm.StartAllServicesSelected();
    }

    public void ButtonRestartService_Click(object sender, EventArgs e)
    {
        if (sm != null)
        {
            sm.StopOneService(((GenerateTextEventArgs)e).EventText);
            sm.StartOneService(((GenerateTextEventArgs)e).EventText);
        }
    }

    public void ButtonStopService_Click(object sender, EventArgs e)
    {
        if (sm != null)
            sm.StopOneService(((GenerateTextEventArgs)e).EventText);
    }

    public void ButtonStartService_Click(object sender, EventArgs e)
    {
        if (sm != null)
            sm.StartOneService(((GenerateTextEventArgs)e).EventText);
    }

    [System.Security.SecurityCritical]
    public void FirstSecurityMethod()
    {
        SecondSecurityMethod();
    }

    [System.Security.SecurityCritical]
    private void SecondSecurityMethod()
    {
        // Assert permissions

        // do privileged actions, such as method call-outs
    }

    protected void TextBox1_PreRender(object sender, EventArgs e)
    {
        UpdateListError();
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {           
            String srvName = GridView1.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text;
            Assembly assem = Assembly.GetExecutingAssembly();
            System.Reflection.MethodInfo Info = this.GetType().GetMethod(e.CommandName);
            Info.Invoke(this, new object[] {sender, new GenerateTextEventArgs(srvName)});
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceWarning(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
    }
}

public class GenerateTextEventArgs : EventArgs
	{
		private string myEventText = null;			

		public GenerateTextEventArgs(string theEventText)
		{			
			if (theEventText == null) throw new NullReferenceException();
			myEventText = theEventText; 
		}
									
		public string EventText
		{
			get { return this.myEventText; }
		}	
	}
