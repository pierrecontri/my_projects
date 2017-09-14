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

public partial class MyComputer : System.Web.UI.Page
{
    private ServerManagment sm = null;
    // filter the requested services
    private static String[] listFilterServices = ConfigurationSettings.AppSettings["ListOfServicesToManage"].Split(new char[] { ';' });
    private static String pathMachineList = ConfigurationSettings.AppSettings["FileOfMachineList"];
    private static String pathZPointCSLog = ConfigurationSettings.AppSettings["ZPointCSLogsPath"];
    private static String pathOracleLog = ConfigurationSettings.AppSettings["OracleLogsPath"];
    private static String strFilterError = ConfigurationSettings.AppSettings["StringFilterError"];
    private static String strFilterWarning = ConfigurationSettings.AppSettings["StringFilterWarning"];
    private static int pathfilterTimeLogs = Int32.Parse(ConfigurationSettings.AppSettings["IntFilterTimeLogs"]);


    protected void Page_Load(object sender, EventArgs e)
    {
        string remoteMachine = Request.Params["remoteMachine"];
        if (remoteMachine != null)
            this.strRemoteMachine.Value = remoteMachine;

        if ("".Equals(this.strRemoteMachine.Value))
            this.strRemoteMachine.Value = Request.UserHostName;

	try 
	{
		this.strRemoteMachine.Value = System.Net.Dns.GetHostEntry(this.strRemoteMachine.Value).HostName;
	}
	catch(Exception /*ioe*/)
	{}

        if (Session["SrvManagment" + this.strRemoteMachine.Value] == null)
            Session["SrvManagment" + this.strRemoteMachine.Value] = new ServerManagment(this.strRemoteMachine.Value, listFilterServices);


        sm = (ServerManagment)Session["SrvManagment" + this.strRemoteMachine.Value];

        try
        {
            if (!sm.MachineName.Equals(this.strRemoteMachine.Value))
                sm.MachineName = this.strRemoteMachine.Value;

            if (sm.ServicesList != null && sm.ServicesList.Count() == 0)
            {
                try
                {
                    Response.Redirect("./FactoryView.aspx", false);
                }
                catch (Exception /*oe*/)
                { }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }

        this.ServicesControl1.ServicesManagment = sm;
        this.ServicesControl1.IsAdmin = true.Equals(Session["isAuthor"]);
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


    protected void TextBoxLogsOracle_Load(object sender, EventArgs e)
    {
        string pathName = this.strRemoteMachine.Value + pathOracleLog;
        string strHyperlink = "file://" + Server.UrlEncode(pathName);
        pathName = "\\\\" + pathName;
        
    }

    protected void TextBoxLogsZPCS_Load(object sender, EventArgs e)
    {
        string pathName = this.strRemoteMachine.Value + pathZPointCSLog + "ZPointCS" + System.DateTime.Now.Date.ToString("yyyyMM") + ".log";
        string strHyperlink = "file://" + Server.UrlEncode(pathName);
        pathName = "\\\\" + pathName;

        string txtLogsFilter = MultiServices.getAnalyseZPLogs(this.sm.MachineName, pathName, DropDownListTypeTraceZP.SelectedValue, pathfilterTimeLogs);
        if ("".Equals(txtLogsFilter))
            txtLogsFilter = "No datas in " + DropDownListTypeTraceZP.SelectedItem.Text;
        this.TextBoxLogsZPCS.Text = txtLogsFilter;

        
    }

    protected void DropDownListTypeTraceZP_SelectedIndexChanged(object sender, EventArgs e)
    {
        /* just for submit */
    }

    protected void Dl_ZPload_Click(object sender, EventArgs e)
    {
        string filename = "ZPointCS" + System.DateTime.Now.Date.ToString("yyyyMM") + ".log";
        string pathName = this.strRemoteMachine.Value + pathZPointCSLog + filename;
        string strHyperlink = "file://" + Server.UrlEncode(pathName);
        pathName = "\\\\" + pathName;

        string txtLogsFilter = MultiServices.getAnalyseZPLogs(this.sm.MachineName, pathName, String.Empty, 0);
        if (String.Empty.Equals(txtLogsFilter))
            txtLogsFilter = "No datas in " + DropDownListTypeTraceZP.SelectedItem.Text;
        try
        {          
			Response.Write(Server.HtmlEncode(txtLogsFilter).Replace("\r\n","<br />\n"));
            Response.Flush();
            Response.End();
        }
        catch (System.Exception ex)
        // file IO errors
        {
            System.Diagnostics.Trace.TraceError(ex.Source + ";" + ex.Message + "\n" + ex.StackTrace);
        }


        //Response.Write(txtLogsFilter.Replace("\n","<br />\n"));
        //Response.End();

              
    }
}