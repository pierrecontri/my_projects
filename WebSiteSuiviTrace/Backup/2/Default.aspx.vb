
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        My.Log.WriteEntry(Date.Now.ToString() + ";test1", Diagnostics.TraceEventType.Information)
        My.Log.WriteEntry(Date.Now.ToString() + ";test2", TraceEventType.Warning)
        My.Log.WriteEntry(Date.Now.ToString() + ";test3", TraceEventType.Error)
        My.Log.WriteEntry(Date.Now.ToString() + ";test4", TraceEventType.Critical)
        My.Log.WriteException(New Exception("Test de l'exception"))
    End Sub
End Class
