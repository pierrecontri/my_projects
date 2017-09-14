<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServicesControl.ascx.cs"
    Inherits="ServicesControl" %>

<table class="ServiceControlContent">
    <tr>
        <td>
            <asp:LinkButton ID="LabelMachineName" CssClass="labelMachine" runat="server"
                OnClick="LabelMachineName_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridViewSerivces" runat="server" OnPreRender="GridViewServices_PreRender"
             AutoGenerateColumns="False" OnRowCommand="GridViewServices_RowCommand" CssClass="ServicesTabs">
                <Columns>
                    <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
                    <asp:BoundField DataField="ServiceName" HeaderText="Service Name" />
                    <asp:BoundField DataField="Status" HeaderText="Status">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:ButtonField CommandName="ButtonRestartService_Click" HeaderText="Restart Service"
                        Text="Restart">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="ButtonStartService_Click" HeaderText="Start Service"
                        Text="Start">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="ButtonStopService_Click" HeaderText="Stop Service"
                        Text="Stop">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                </Columns>
            </asp:GridView>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="TextBoxErrors" runat="server" ReadOnly="True" Rows="4" class="txtErrors"
                            TextMode="MultiLine" OnPreRender="TextBoxErrors_PreRender" Width="470px" 
                            onload="TextBoxErrors_Load">Pas 
        d&#39;erreurs détectées</asp:TextBox>
                    </td>
                    <%--<td>
                        <asp:Button ID="ButtonRestartServices" runat="server" class="buttons" OnClick="ButtonRestartServices_Click"
                            Text="Repair the machine" />
                    </td>--%>
                </tr>
            </table>
        </td>
    </tr>
</table>
