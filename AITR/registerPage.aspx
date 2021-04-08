<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="registerPage.aspx.cs" Inherits="AITR.registerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/registerPage.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Label ID="registerPageLabel" runat="server" Text="Register Page"></asp:Label>
        <br />
        <br />
        <asp:TextBox class="textBox" ID="firstName" runat="server" placeholder="first name"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox class="textBox" ID="lasttName" runat="server" placeholder="last name"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox class="textBox" ID="dob" runat="server" placeholder="date of birth"></asp:TextBox>
        <br />
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        <br />
        <br />
        <asp:TextBox class="textBox" ID="phoneNumber" runat="server" placeholder="phone number"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox class="textBox" ID="email" runat="server" placeholder="email"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="register" runat="server" Text="Register" />
        
        <br />
        <asp:Button ID="cancel" runat="server" Text="Cancel" />
        <br />
    </div>
</asp:Content>
