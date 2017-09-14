using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ServicesViewerControl : System.Web.UI.UserControl
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

    protected void GridViewServices_PreRender(object sender, EventArgs e)
    {
        GridViewSerivces.DataBind();
    }
}
