<%@ Application Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Linq" %>

<script runat="server">

    private static string[] strAuthors = ConfigurationSettings.AppSettings["strAuthors"].Split(new char[] {';'});
    void Application_Start(object sender, EventArgs e) 
    {
        // Code qui s'exécute au démarrage de l'application

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code qui s'exécute à l'arrêt de l'application

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code qui s'exécute lorsqu'une erreur non gérée se produit

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code qui s'exécute lorsqu'une nouvelle session démarre
        Response.Write("Global.asax; UserLogonIdentity : " + Request.LogonUserIdentity.Name + "<br />");
        string[] grps = GetGroups();
        Session["strAuthors"] = !((Int32)0).Equals(grps.Intersect(strAuthors).Count());
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code qui s'exécute lorsqu'une session se termine. 
        // Remarque : l'événement Session_End est déclenché uniquement lorsque le mode sessionstate
        // a la valeur InProc dans le fichier Web.config. Si le mode de session a la valeur StateServer 
        // ou SQLServer, l'événement n'est pas déclenché.

    }


    /// <summary>
    /// Get a list of all of the groups the current
    /// user is a member of to support test of 
    /// MyLifeSpaceAdmin membership
    /// </summary>
    /// <returns></returns>
    public string[] GetGroups()
    {
        List<string> groups = new List<string>();
        foreach (System.Security.Principal.IdentityReference group in
        System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups)
        {
            groups.Add(group.Translate(typeof
            (System.Security.Principal.NTAccount)).ToString());
        }
        return groups.ToArray();
    }
       
</script>
