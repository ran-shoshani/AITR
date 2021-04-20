<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="errorPage.aspx.cs" Inherits="AITR.errorpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/errorPage.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Label ID="errorLabel" runat="server" Text="OOPS ....ERROR!!!"></asp:Label>
        <asp:Button ID="cancelButton" runat="server" Text="Go Back" OnClick="cancelButton_Click" />

        <iframe src="https://giphy.com/embed/TRkCyFl4eolq0" width="250" height="220" frameborder="0" class="giphy-embed" allowfullscreen></iframe>
        <p><a href="https://giphy.com/gifs/drake-funny-gif-TRkCyFl4eolq0">via GIPHY</a></p>
    </div>


</asp:Content>
