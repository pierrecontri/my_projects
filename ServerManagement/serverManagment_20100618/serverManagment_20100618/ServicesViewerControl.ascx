<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServicesViewerControl.ascx.cs" Inherits="ServicesViewerControl" %>
<table class="ServiceControlContent">
    <tr>
        <td>
            <asp:Label ID="LabelMachineName" CssClass="labelMachine" runat="server" 
                Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridViewSerivces" runat="server" OnPreRender="GridViewServices_PreRender"
             AutoGenerateColumns="False" CssClass="ServicesTabsView">
        <Columns>
            <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
            <asp:BoundField DataField="ServiceName" HeaderText="Service Name" />
            <asp:BoundField DataField="Status" HeaderText="Status" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>

        </td>
    </tr>
</table>
