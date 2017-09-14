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
    private ServerManagment sm = null;
    // filter the requested services
    private static String[] listFilterServices = ConfigurationSettings.AppSettings["ListOfServicesToManage"].Split(new char[] { ';' });
    private static String pathMachineList = ConfigurationSettings.AppSettings["FileOfMachineList"];
    private static String pathZPointCSLog = ConfigurationSettings.AppSettings["ZPointCSLogsPath"];
    private static String pathOracleLog = ConfigurationSettings.AppSettings["OracleLogsPath"];
    private static String lettre = ConfigurationSettings.AppSettings["lettre"];
    private static String lettre2 = ConfigurationSettings.AppSettings["lettre2"];
    private static int pathfilterTimeLogs = Int32.Parse(ConfigurationSettings.AppSettings["filterTimeLogs"]);


    protected void Page_Load(object sender, EventArgs e)
    {
        string remoteMachine = Request.Params["remoteMachine"];
        if (remoteMachine != null)
            this.strRemoteMachine.Value = remoteMachine;

        if ("".Equals(this.strRemoteMachine.Value))
            this.strRemoteMachine.Value = Request.UserHostName;



        if (Session["SrvManagment"] == null)
            Session["SrvManagment"] = new ServerManagment(this.strRemoteMachine.Value, listFilterServices);

        sm = (ServerManagment)Session["SrvManagment"];

        try
        {
            if (!sm.MachineName.Equals(this.strRemoteMachine.Value))
                sm.MachineName = this.strRemoteMachine.Value;

            if (sm.ServicesList != null && sm.ServicesList.Count() == 0)
                Response.Redirect("./FactoryView.aspx");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }

        this.ServicesControl1.ServicesManagment = sm;
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
        string pathName = "\\\\" + Request.UserHostName + pathOracleLog;
        this.TextBoxLogsOracle.Text = MultiServices.getAnalyseOracleLogs(this.sm.MachineName, pathName);
        this.HyperLinkOracle.NavigateUrl = pathName;

    }


    protected void TextBoxLogsZPCS_Load(object sender, EventArgs e)
    {
        string pathName = "\\\\" + Request.UserHostName + pathZPointCSLog + "ZPointCS" + System.DateTime.Now.Date.ToString("yyyyMM") + ".log";

        

       
        
        //this.TextBoxLogsZPCS.Text = MultiServices.getAnalyseZPLogs(this.sm.MachineName, pathName,4);



        if (CheckBox1.Checked && CheckBox2.Checked)
        {
            this.TextBoxLogsZPCS.Text = MultiServices.getErrorZPLogs(this.sm.MachineName, pathName, lettre) + MultiServices.getWarningZPLogs(this.sm.MachineName, pathName, lettre2);
        }
        else if (CheckBox1.Checked && !CheckBox2.Checked) // on cherche uniquement les erreurs
        {
            this.TextBoxLogsZPCS.Text = MultiServices.getErrorZPLogs(this.sm.MachineName, pathName, lettre);
        }
        else if (!CheckBox1.Checked && CheckBox2.Checked) // On cherche uniquement les warning
        {
            this.TextBoxLogsZPCS.Text = MultiServices.getWarningZPLogs(this.sm.MachineName, pathName, lettre2);
        }
        else this.TextBoxLogsZPCS.Text = MultiServices.getAnalyseZPLogs(this.sm.MachineName, pathName);

        this.HyperLinkZP.NavigateUrl = pathName;   
    }

}