<%@ Application Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code qui s'exécute au démarrage de l'application

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code qui s'exécute à l'arrêt de l'application

    }


    //void Application_Error(object sender, EventArgs e)
    //{
       
        //Exception exc = Server.GetLastError();
        //if (exc.GetType() == typeof(HttpException))
        //{
        //    if (exc.Message.Contains("denied"))

        //    Server.Transfer("FactoryView.aspx");
        //     Response.Write("<h2>Global Page Error</h2>\n");
        //Response.Write(
        //    "<p>" + exc.Message+ "</p>\n");
        //Response.Write("Return to the <a href='FactoryView.aspx'>" +
        //    "Default Page</a>\n");
//  // Clear the error from the server
//        Server.ClearError();
//}
        
  //      protected void Application_EndRequest(Object sender, 
  //                                           EventArgs e)
  //{
  //    try
  //    {
  //        Exception exc = Server.GetLastError();
  //        if (exc.GetType() == typeof(HttpException))
  //        {
  //            if (exc.Message.Contains("denied"))
  //            {
  //                Response.ClearContent();
  //                Server.Execute("FactoryView.aspx");

  //            }
  //        }
  //    }
  //    catch (Exception ex)
  //    {
  //        System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
  //    }
  //}
        

private void Application_EndRequest(Object source, EventArgs e)
{
 HttpApplication httpApp;
 HttpContext httpContext;
 try
 {
  httpApp = (HttpApplication)source;
  httpContext = httpApp.Context;
  if (httpContext.Response.StatusCode.ToString().Equals("401"))
  {

      httpContext.Response.Redirect("http://170.56.146.9/FactoryView.aspx");

  }
 }
 catch (Exception ex)
 {
     System.Diagnostics.Trace.TraceError(ex.Source + "; " + ex.Message + "\n" + ex.StackTrace);
 }
}


       

 





   

    void Session_Start(object sender, EventArgs e) 
    {
        // Code qui s'exécute lorsqu'une nouvelle session démarre
        //Trace.TraceInformation("Entrée dans l'application de l'utilisateur : " + User.Identity.Name);
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code qui s'exécute lorsqu'une session se termine. 
        // Remarque : l'événement Session_End est déclenché uniquement lorsque le mode sessionstate
        // a la valeur InProc dans le fichier Web.config. Si le mode de session a la valeur StateServer 
        // ou SQLServer, l'événement n'est pas déclenché.
        //Trace.TraceInformation("Sortie de l'application de l'utilisateur : " + User.Identity.Name);
    }
       
</script>
