<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServicesViewerControl.ascx.cs" Inherits="ServicesViewerControl" %>
<table style="width:100%;">
    <tr>
        <td>
            <asp:Label ID="LabelMachineName" CssClass="labelMachine" runat="server" 
                Text="Label" Width="594px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridViewSerivces" runat="server" onprerender="GridViewServices_PreRender" 
        CellPadding="4" ForeColor="#333333" GridLines="None" 
        AutoGenerateColumns="False">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
            <asp:BoundField DataField="ServiceName" HeaderText="Service Name" />
            <asp:BoundField DataField="Status" HeaderText="Status" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>

        </td>
    </tr>
</table>
