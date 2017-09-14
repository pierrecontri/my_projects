<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" validateRequest="false"%>
<%@ Register src="ServicesControl.ascx" tagname="ServicesControl" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Managment of <%=Request.UserHostName %></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc1:ServicesControl ID="ServicesControl1" runat="server" />
    <br />
    <h6>Logs ZPointCS</h6>
    <br />
    <asp:TextBox ID="TextBoxLogsZPCS" runat="server" Height="250px" 
        TextMode="MultiLine" Width="700px" onload="TextBoxLogsZPCS_Load" 
        style="margin-top: 0px; margin-bottom: 0px"></asp:TextBox>
   <br /> 
    <asp:CheckBox ID="CheckBox1" runat="server" 
        oncheckedchanged="TextBoxLogsZPCS_Load" Text="<h6>Error</h6>" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:CheckBox ID="CheckBox2" runat="server" 
        oncheckedchanged="TextBoxLogsZPCS_Load" Text="<h6>Warning</h6>" />
            <h6>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Le</h6> &nbsp;&nbsp;<asp:TextBox 
        ID="TextBoxDate1" runat="server" Width="66px"></asp:TextBox>
&nbsp;&nbsp;<h6>(yyyy-mm-dd)</h6>&nbsp;
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" onclick="TextBoxLogsZPCS_Load" 
    Text="Rechercher" />
    <br />

    <br /><asp:HyperLink ID="HyperLinkZP" runat="server">Download ZPointCS log file</asp:HyperLink>
    <br />
    <br />
    <br />
    <h6>Logs Oracle</h6>
       <br />
    <asp:TextBox ID="TextBoxLogsOracle" runat="server" Height="100px" TextMode="MultiLine" 
        Width="700px" onload="TextBoxLogsOracle_Load"></asp:TextBox>
    <br /><asp:HyperLink ID="HyperLinkOracle" runat="server">Downlaod Oracle log 
    file</asp:HyperLink>
    <asp:HiddenField ID="strRemoteMachine" runat="server" Value="" />
</asp:Content>