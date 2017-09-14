<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MyComputer.aspx.cs" Inherits="MyComputer" ValidateRequest="false" %>

<%@ Register Src="ServicesControl.ascx" TagName="ServicesControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Managment of
        <%=System.Net.Dns.GetHostEntry(this.strRemoteMachine.Value).HostName %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ServicesControl ID="ServicesControl1" runat="server" />
    <br />
    <div class="ServiceControlContent">
        <div class="titleContent">
            Logs ZPointCS</div>
        <br />
        <asp:TextBox ID="TextBoxLogsZPCS" runat="server" Height="250px" TextMode="MultiLine"
            Width="700px" OnLoad="TextBoxLogsZPCS_Load"></asp:TextBox>
        <br />
        Choose your Error Type :&nbsp;
        <asp:DropDownList ID="DropDownListTypeTraceZP" runat="server" AutoPostBack="True"
            OnSelectedIndexChanged="DropDownListTypeTraceZP_SelectedIndexChanged">
            <asp:ListItem Value="0">All</asp:ListItem>
            <asp:ListItem Value="4">Info</asp:ListItem>
            <asp:ListItem Value="5">Warning</asp:ListItem>
            <asp:ListItem Value="6">Error</asp:ListItem>
            <asp:ListItem Value="7">Critical</asp:ListItem>
            <asp:ListItem Selected="True" Value="--&gt;">Sequence</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Dl_ZPload" runat="server" OnClick="Dl_ZPload_Click" Text="Download&nbsp;ZP&nbsp;logs"
            CssClass="buttons" />
        <br />
    </div>
    <br />
    <div class="ServiceControlContent">
        <div class="titleContent">
            Logs Oracle</div>
        <br />
        <asp:TextBox ID="TextBoxLogsOracle" runat="server" TextMode="MultiLine" OnLoad="TextBoxLogsOracle_Load"></asp:TextBox>
        <br />
        <asp:HyperLink ID="HyperLinkOracle" runat="server">Download Oracle log file</asp:HyperLink>
        <br />
    </div>
    <asp:HiddenField ID="strRemoteMachine" runat="server" Value="" />
</asp:Content>
