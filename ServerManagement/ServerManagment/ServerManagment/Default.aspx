<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Page sans titre</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="GridView1" runat="server" onprerender="GridView1_PreRender" 
        CellPadding="4" ForeColor="#333333" GridLines="None" 
        AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
            <asp:BoundField DataField="ServiceName" HeaderText="Service Name" />
            <asp:BoundField DataField="Status" HeaderText="Status" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:ButtonField CommandName="ButtonRestartService_Click" 
                HeaderText="Restart Service" Text="Restart" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:ButtonField>
            <asp:ButtonField CommandName="ButtonStartService_Click" 
                HeaderText="Start Service" Text="Start" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:ButtonField>
            <asp:ButtonField CommandName="ButtonStopService_Click" 
                HeaderText="Stop Service" Text="Stop" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:ButtonField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" Rows="4" 
        TextMode="MultiLine" onprerender="TextBox1_PreRender" Width="470px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="ButtonRestartServices" runat="server" 
        onclick="ButtonRestartServices_Click" Text="Restart All Services" />
    <asp:Button ID="ButtonStartServices" runat="server" 
        onclick="ButtonStartServices_Click" Text="Start All Services" />
    <asp:Button ID="ButtonStopServices" runat="server" 
        onclick="ButtonStopServices_Click" Text="Stop All Services" />
    <br />
    </form>
</body>
</html>
