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
    private bool _isManager = false;

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
        GridViewSerivces.RowDataBound += new GridViewRowEventHandler(GridViewSerivces_RowDataBound);

        try
        {
            this.LabelMachineName.Text = ServicesManagment.MachineName;
            this.LabelMachineName.PostBackUrl = "MyComputer.aspx?remoteMachine=" + ServicesManagment.MachineName;
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

    public bool IsAdmin
    {
        get { return _isManager; }
        set { _isManager = value; }
    }

    protected void UpdateListError()
    {
        if (ServicesManagment != null)
            TextBoxErrors.Text = ServicesManagment.GetAllErrors();
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
    protected void LabelMachineName_Click(object sender, EventArgs e)
    {

    }
    protected void GridViewSerivces_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.Cells.Count < 3) return;
            if ("Running".Equals(e.Row.Cells[2].Text))
            {
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Green;
            }
            else if ("Stopped".Equals(e.Row.Cells[2].Text))
            {
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception /* ieo*/)
        { }
    }



    protected void TextBoxErrors_Load(object sender, EventArgs e)
    {
        try
        {
            this.TextBoxErrors.Visible = (this.ServicesManagment != null && this.ServicesManagment.ServicesList.Count() > 0);
        }
        catch (Exception /*ioe*/)
        { }
    }

    protected void GridViewSerivces_Load(object sender, EventArgs e)
    {
        try
        {
            if (this.GridViewSerivces.Columns.Count > 3) // presence de 1 a n boutons
            {
                for (int i = 3; i < this.GridViewSerivces.Columns.Count; i++)
                {
                    this.GridViewSerivces.Columns[i].Visible = IsAdmin;
                }
            }
            this.GridViewSerivces.CssClass = (IsAdmin) ? "ServicesTabs" : "ServicesTabsView";
        }
        catch (Exception /*ioe*/)
        { }
    }
    protected void HyperLinkMachineDetails_Load(object sender, EventArgs e)
    {
        try
        {
            HyperLink detailsLink = (HyperLink)sender;
            detailsLink.NavigateUrl += "?remoteMachine=" + this._sm.MachineName;
            detailsLink.Target += this._sm.MachineName;
        }
        catch (Exception /* ex */) { }
    }
}