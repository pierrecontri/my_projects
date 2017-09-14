using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("Request.LogonUserIdentity : " + Request.LogonUserIdentity.Name + "<br/>");
        Response.Write("User.Identity : " + User.Identity.Name + "<br/>");

        foreach (var tty in Request.LogonUserIdentity.Groups)
        {
            Response.Write(tty.Value + "<br/>");
        }

        var test = new System.Security.Principal.WindowsPrincipal(Request.LogonUserIdentity);
        Context.User = test;

        RoleGroup rg = new RoleGroup();
        rg.Roles = new string[] { "Administrateurs", "Utilisators" };

        Response.Write("Roles containt user: " + rg.ContainsUser(test) + "<br/>");

        // Create stream writer object and pass it the file path
        System.IO.StreamWriter sw = new System.IO.StreamWriter(Response.OutputStream);

        // Write user info to log
        sw.WriteLine("Access log from " + DateTime.Now.ToString() + "<br/>");
        sw.WriteLine("User: " + Request.LogonUserIdentity.User + "<br/>");
        sw.WriteLine("Name: " + Request.LogonUserIdentity.Name + "<br/>");
        sw.WriteLine("AuthenticationType: " + Request.LogonUserIdentity.AuthenticationType + "<br/>");
        sw.WriteLine("ImpersonationLevel: " + Request.LogonUserIdentity.ImpersonationLevel + "<br/>");
        sw.WriteLine("IsAnonymous: " + Request.LogonUserIdentity.IsAnonymous + "<br/>");
        sw.WriteLine("IsGuest: " + Request.LogonUserIdentity.IsGuest + "<br/>");
        sw.WriteLine("IsSystem: " + Request.LogonUserIdentity.IsSystem + "<br/>");
        sw.WriteLine("Owner: " + Request.LogonUserIdentity.Owner + "<br/>");
        sw.WriteLine("Token: " + Request.LogonUserIdentity.Token + "<br/>");

        // Close the stream to the file.
        sw.Close();

        ArrayList getGrps = GetGroups();
        foreach (var ttu in getGrps)
        {
            Response.Write(ttu.ToString() + "<br/>");
        }

        Response.Write("<h1>" + Session["strAuthors"] + "</h1>");
    }

    /// <summary>
    /// Get a list of all of the groups the current
    /// user is a member of to support test of 
    /// MyLifeSpaceAdmin membership
    /// </summary>
    /// <returns></returns>
    public ArrayList GetGroups()
    {
        ArrayList groups = new ArrayList();
        foreach (System.Security.Principal.IdentityReference group in
        System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups)
        {
            groups.Add(group.Translate(typeof
            (System.Security.Principal.NTAccount)).ToString());
        }
        return groups;
    }

}
