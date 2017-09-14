<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register src="ServicesControl.ascx" tagname="ServicesControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Managment of <%=Request.UserHostName %></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc1:ServicesControl ID="ServicesControl1" runat="server" />
    <br />
    Logs ZPointCS<br />
    <br />
    <asp:TextBox ID="TextBoxLogsZPCS" runat="server" Height="250px" 
        TextMode="MultiLine" Width="700px" onload="TextBoxLogsZPCS_Load"></asp:TextBox>
    <br /><asp:HyperLink ID="HyperLinkZP" runat="server">Download ZPointCS log file</asp:HyperLink>
    <br /><br />
    Logs Oracle
    <br />
    <br />
    <asp:TextBox ID="TextBoxLogsOracle" runat="server" Height="100px" TextMode="MultiLine" 
        Width="700px" onload="TextBoxLogsOracle_Load"></asp:TextBox>
    <br /><asp:HyperLink ID="HyperLinkOracle" runat="server">Downlaod Oracle log file</asp:HyperLink>
    <asp:HiddenField ID="strRemoteMachine" runat="server" Value="" />
</asp:Content>