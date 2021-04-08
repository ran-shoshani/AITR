<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="errorPage.aspx.cs" Inherits="AITR.errorpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="styles/errorPage.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
         <asp:Label ID="errorLabel" runat="server" Text="OOPS ....ERROR!!!"></asp:Label>
         <asp:Button ID="cancelButton" runat="server" Text="Go Back To Start Page" OnClick="cancelButton_Click" />
    </div>
        
   
</asp:Content>
