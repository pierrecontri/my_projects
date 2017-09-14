<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Mail.aspx.cs" Inherits="Mail" Title="Mail Send" validateRequest="false"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Mail Sending</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="FormMail">  
    <asp:Label ID="LabelExpediteur" runat="server" Text="<h6>Expediteur:</h6>"></asp:Label>
&nbsp;
    <asp:TextBox ID="TBExpediteur" runat="server" Width="209px" 
        style="margin-left: 8px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="LabelObjet" runat="server" Text="<h6>Objet:</h6>"></asp:Label>
&nbsp;
    <asp:TextBox ID="TBObjet" runat="server" Width="209px" 
        style="margin-left: 39px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="LabelMessage" runat="server" Text="<h6>Message:</h6>"></asp:Label>
    <asp:TextBox ID="TBMessage" runat="server" Height="101px" Width="358px"></asp:TextBox>
    <br />
&nbsp;&nbsp; 
    <br />
    <br />
    <asp:Button ID="ButtonSend" runat="server" Text="Envoyer" />
</div>
</asp:Content>

