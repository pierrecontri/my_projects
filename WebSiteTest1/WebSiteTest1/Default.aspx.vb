Imports System
Imports System.Web.UI
Imports System.Threading


Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tab As New Table()
        For i As Integer = 0 To 10
            Dim row As New TableRow()
            Dim cell1 As New TableCell()
            Dim cell2 As New TableCell()

            cell1.Text = "Hello " + i.ToString()
            cell2.Text = "bye !"

            row.Controls.Add(cell1)
            row.Controls.Add(cell2)

            tab.Controls.Add(row)
        Next
        Me.Page.Controls.Add(tab)
    End Sub
End Class
