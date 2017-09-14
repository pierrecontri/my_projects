using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Trace.Write(DateTime.Now.ToString() + ";Test de trace .Net C# 2.0");
        System.Diagnostics.Trace.TraceInformation("Test 6");
        //System.Diagnostics.Trace.Flush();
    }
}
