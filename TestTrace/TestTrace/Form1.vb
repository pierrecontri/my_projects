Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Trace.Listeners.Add(Debug.Listeners(0))

        My.Application.Log.WriteEntry("Test entrée", TraceEventType.Error)
        Trace.TraceWarning("Entrée Warning")
        Debug.WriteLine("Entrée appli debug")
    End Sub
End Class
