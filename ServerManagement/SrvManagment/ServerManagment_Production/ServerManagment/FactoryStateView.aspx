<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FactoryStateView.aspx.cs" Inherits="FactoryStateView" Title="Page sans titre" validateRequest="false" %>
<%@ Register src="ServicesControl.ascx" tagname="ServicesControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Factory State View</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="ContentPanel" runat="server" style="margin-left: 0px" />
</asp:Content>

