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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SrvManagment"] == null)
        {
            sm = new ServerManagment(Request.UserHostName, listFilterServices);
            Session["SrvManagment"] = sm;
        }
        else
        {
            sm = (ServerManagment)Session["SrvManagment"];
        }

        if (((Int32) 0).Equals(sm.ServicesList.Count()))
            Response.Redirect("./FactoryView.aspx");

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

    protected void TextBoxLogsZPCS_Load(object sender, EventArgs e)
    {
        string pathName = "\\\\" + Request.UserHostName + pathZPointCSLog + "ZPointCS" + System.DateTime.Now.Date.ToString("yyyyMM") + ".log";
        this.TextBoxLogsZPCS.Text = MultiServices.getAnalyseZPLogs(this.sm.MachineName, pathName);
        this.HyperLinkZP.NavigateUrl = pathName;
    }

    protected void TextBoxLogsOracle_Load(object sender, EventArgs e)
    {
        string pathName = "\\\\" + Request.UserHostName + pathOracleLog;
        this.TextBoxLogsOracle.Text = MultiServices.getAnalyseOracleLogs(this.sm.MachineName, pathName);
        this.HyperLinkOracle.NavigateUrl = pathName;
    }
}