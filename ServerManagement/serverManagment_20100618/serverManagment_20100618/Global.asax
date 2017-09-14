<%@ Application Language="C#" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Diagnostics" %>

<script RunAt="server">
    
    void Application_Start(object sender, EventArgs e)
    {
        // Code qui s'exécute au démarrage de l'application

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code qui s'exécute à l'arrêt de l'application

    }

    private static string[] grpsAdmis = ConfigurationSettings.AppSettings["ListeOfUsersAccess"].Split(new char[] { ';' });
    void Session_Start(object sender, EventArgs e)
    {
        // Code qui s'exécute lorsqu'une nouvelle session démarre
        Trace.TraceInformation("Entrée dans l'application de l'utilisateur : " + User.Identity.Name);
        // test et recuperation du type d'utilisateur (isAuthor)
        Session["isAuthor"] = false;
        try
        {
            // check rights
            string[] grpsAuthor = grpsAdmis.Where(User.IsInRole).ToArray();
            Session["isAuthor"] = grpsAuthor.Count() != 0;
        }
        catch (Exception /*ieo*/)
        { }
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code qui s'exécute lorsqu'une session se termine. 
        // Remarque : l'événement Session_End est déclenché uniquement lorsque le mode sessionstate
        // a la valeur InProc dans le fichier Web.config. Si le mode de session a la valeur StateServer 
        // ou SQLServer, l'événement n'est pas déclenché.
        Trace.TraceInformation("Sortie de l'application de l'utilisateur : " + User.Identity.Name);
    }
       
</script>

