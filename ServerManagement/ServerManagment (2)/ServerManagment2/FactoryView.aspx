<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="FactoryView.aspx.cs" Inherits="FactoryView" Title="View of Factory" %>

<%@ Register src="ServicesControl.ascx" tagname="ServicesControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Factory's View</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="ContentPanel" runat="server" />        
</asp:Content>