#define TRACE

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    private static string strPublicPageName = ConfigurationSettings.AppSettings["PublicPageName"];

    protected override void OnLoad(EventArgs e)
    {
        // if nok, redirect
        System.Diagnostics.Trace.TraceWarning(HttpContext.Current.User.Identity.Name + " : " + Session["isAuthor"].ToString());
        //if (false.Equals(Session["isAuthor"]) && !Request.Url.PathAndQuery.Contains(strPublicPageName))
        //{
        //    try
        //    {
        //        Response.Redirect(strPublicPageName, false);
        //        return;
        //    }
        //    catch (Exception /*oe*/)
        //    { }
        //}
        base.OnLoad(e);
    }

}
