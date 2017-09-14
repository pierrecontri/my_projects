<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<%@ Register Src="ServicesControl.ascx" TagName="ServicesControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Managment of
        <%=Request.UserHostName %></title>
</asp:Content>
<asp:Content ID="Content2"  ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
    <uc1:ServicesControl ID="ServicesControl1" runat="server" /> 
    <br />
       Logs ZPointCS
    <br />
    <asp:TextBox ID="TextBoxLogsZPCS" runat="server"  Height="250px" TextMode="MultiLine"
        Width="700px" OnLoad="TextBoxLogsZPCS_Load"></asp:TextBox>
    <br />
    <%--
    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="TextBoxLogsZPCS_Load"
        Text="Error" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="TextBoxLogsZPCS_Load"
        Text="Warning" />
    Le&nbsp;&nbsp;<asp:TextBox ID="TextBoxDate1" runat="server"></asp:TextBox>
    (yyyy-mm-dd)
    <asp:Button ID="Button1" runat="server" CssClass="buttons" OnClick="TextBoxLogsZPCS_Load"
        Text="Rechercher" />
        --%>
    Choose your Error Type :&nbsp;
    <asp:DropDownList ID="DropDownListTypeTraceZP" runat="server" 
        AutoPostBack="True" 
        onselectedindexchanged="DropDownListTypeTraceZP_SelectedIndexChanged">
        <asp:ListItem Value="0">All</asp:ListItem>
        <asp:ListItem Value="4">Info</asp:ListItem>
        <asp:ListItem Value="5">Warning</asp:ListItem>
        <asp:ListItem Selected="True" Value="6">Error</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:HyperLink ID="HyperLinkZP" runat="server">Download ZPointCS log file</asp:HyperLink>
    <br />
    <br />
    <br />
        Logs Oracle
    <br />
    <asp:TextBox ID="TextBoxLogsOracle" runat="server" TextMode="MultiLine"
        OnLoad="TextBoxLogsOracle_Load"></asp:TextBox>
    <br />
    <asp:HyperLink ID="HyperLinkOracle" runat="server">Download Oracle log file</asp:HyperLink>

    <asp:HiddenField ID="strRemoteMachine" runat="server" Value="" />
</asp:Content>
