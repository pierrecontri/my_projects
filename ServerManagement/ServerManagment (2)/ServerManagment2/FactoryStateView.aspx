<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FactoryStateView.aspx.cs" Inherits="FactoryStateView" Title="Page sans titre" %>

<%@ Register src="ServicesViewerControl.ascx" tagname="ServicesViewerControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Factory State View</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="ContentPanel" runat="server" />
</asp:Content>
