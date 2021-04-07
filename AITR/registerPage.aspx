<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="registerPage.aspx.cs" Inherits="AITR.registerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/registerPage.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Label ID="registerPageLabel" runat="server" Text="Register Page"></asp:Label>
        <br />
        <asp:PlaceHolder ID="AnswerList" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>
