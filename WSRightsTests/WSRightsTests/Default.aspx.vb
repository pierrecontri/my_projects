
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tty As System.Security.Principal.WindowsIdentity = Me.Request.LogonUserIdentity
        Dim grps() As String = {"Administrateurs", "Utilisateurs"}
        TextBox1.Text = tty.Name + vbCrLf
        Dim resp() As String = grps.Where(AddressOf User.IsInRole).ToArray()
        Dim to2 As New System.Security.Principal.NTAccount("Loutre", "Administrateurs")
        TextBox1.Text += User.IsInRole("Utilisateurs").ToString + vbCrLf
        TextBox1.Text += to2.Value + vbCrLf
        TextBox1.Text += String.Join(vbCrLf, resp)
    End Sub

End Class
