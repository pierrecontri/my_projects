using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security;
using System.Security.Permissions;

public partial class ServicesControl : System.Web.UI.UserControl  
{
    private ServerManagment _sm = null;

    protected override void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
    }

    protected void InitializeComponent()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.LabelMachineName.Text = ServicesManagment.MachineName;
            this.GridViewSerivces.DataMember = "ServerManagment";
            this.GridViewSerivces.DataSource = ServicesManagment.ServicesList;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
    }

    public ServerManagment ServicesManagment
    {
        get { return _sm; }
        set { _sm = value; }
    }

    protected void UpdateListError()
    {
        if (ServicesManagment != null)
            TextBoxErrors.Text = ServicesManagment.GetLastErrors();
    }

    protected void TextBoxErrors_PreRender(object sender, EventArgs e)
    {
        UpdateListError();
    }

    protected void GridViewServices_PreRender(object sender, EventArgs e)
    {
        GridViewSerivces.DataBind();
    }

    protected void GridViewServices_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            String srvName = GridViewSerivces.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text;
            Assembly assem = Assembly.GetExecutingAssembly();
            System.Reflection.MethodInfo Info = this.GetType().GetMethod(e.CommandName);
            Info.Invoke(this, new object[] { sender, new GenerateTextEventArgs(srvName) });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceWarning(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
        }
    }

    [System.Security.SecurityCritical]
    protected void ButtonRestartServices_Click(object sender, EventArgs e)
    {
        if (ServicesManagment != null)
        {
            ServicesManagment.StopAllServicesSelected();
            ServicesManagment.StartAllServicesSelected();
        }
    }

    [System.Security.SecurityCritical]
    protected void ButtonStopServices_Click(object sender, EventArgs e)
    {
        if (ServicesManagment != null)
            ServicesManagment.StopAllServicesSelected();
    }

    [System.Security.SecurityCritical]
    protected void ButtonStartServices_Click(object sender, EventArgs e)
    {
        if (ServicesManagment != null)
            ServicesManagment.StartAllServicesSelected();
    }

    [System.Security.SecurityCritical]
    public void ButtonRestartService_Click(object sender, EventArgs e)
    {
        if (ServicesManagment != null)
        {
            ServicesManagment.StopOneService(((GenerateTextEventArgs)e).EventText);
            ServicesManagment.StartOneService(((GenerateTextEventArgs)e).EventText);
        }
    }

    [System.Security.SecurityCritical]
    public void ButtonStopService_Click(object sender, EventArgs e)
    {
        if (ServicesManagment != null)
            ServicesManagment.StopOneService(((GenerateTextEventArgs)e).EventText);
    }

    [System.Security.SecurityCritical]
    public void ButtonStartService_Click(object sender, EventArgs e)
    {
        if (ServicesManagment != null)
            ServicesManagment.StartOneService(((GenerateTextEventArgs)e).EventText);
    }
}