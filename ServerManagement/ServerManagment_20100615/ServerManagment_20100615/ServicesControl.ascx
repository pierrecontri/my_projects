<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServicesControl.ascx.cs" Inherits="ServicesControl" %>
<table style="width:100%;">
    <tr>
    <div id = ongletname>
            <td>
            <asp:LinkButton ID="LabelMachineName"  CssClass="labelMachine" runat="server" 
                Width="385px" onclick="LabelMachineName_Click"></asp:LinkButton>             
        </td>
        </div>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridViewSerivces" runat="server" onprerender="GridViewServices_PreRender" 
        CellPadding="4" ForeColor="#333333" GridLines="None" 
        AutoGenerateColumns="False" onrowcommand="GridViewServices_RowCommand">
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

    <table>
    <tr>
    <td>
    <asp:TextBox ID="TextBoxErrors" runat="server" ReadOnly="True" Rows="4" CssClass="txtErrors"
        TextMode="MultiLine" onprerender="TextBoxErrors_PreRender" Width="470px">Pas 
        d&#39;erreurs détectées</asp:TextBox>
        </td>
        <td>
    <asp:Button ID="ButtonRestartServices" runat="server"  CssClass="btnManage"
        onclick="ButtonRestartServices_Click" Text="Repair the machine" />
        </td>
        </tr>
    </table>

        </td>
    </tr>
</table>
