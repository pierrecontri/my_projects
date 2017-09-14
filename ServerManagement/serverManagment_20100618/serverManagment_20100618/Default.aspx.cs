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
        string pathName = Request.UserHostName + pathOracleLog;
        string strHyperlink = "file://" + Server.UrlEncode(pathName);
        pathName = "\\\\" + pathName;

        //this.TextBoxLogsOracle.Text = MultiServices.getAnalyseOracleLogs(this.sm.MachineName, pathName);
        this.TextBoxLogsOracle.Text = HttpContext.Current.User.Identity.Name;
        this.HyperLinkOracle.NavigateUrl = strHyperlink;
    }

    protected void TextBoxLogsZPCS_Load(object sender, EventArgs e)
    {
        string pathName = Request.UserHostName + pathZPointCSLog + "ZPointCS" + System.DateTime.Now.Date.ToString("yyyyMM") + ".log";
        string strHyperlink = "file://" + Server.UrlEncode(pathName);
        pathName = "\\\\" + pathName;

        string txtLogsFilter = MultiServices.getAnalyseZPLogs(this.sm.MachineName, pathName, DropDownListTypeTraceZP.SelectedValue, pathfilterTimeLogs);
        if ("".Equals(txtLogsFilter))
            txtLogsFilter = "No datas in " + DropDownListTypeTraceZP.SelectedItem.Text;
        this.TextBoxLogsZPCS.Text = txtLogsFilter;

        /*
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
        */

        this.HyperLinkZP.NavigateUrl = pathName;
    }

    protected void DropDownListTypeTraceZP_SelectedIndexChanged(object sender, EventArgs e)
    {
        /* just for submit */
    }
}