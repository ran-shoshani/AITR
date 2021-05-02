<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="registerPage.aspx.cs" Inherits="AITR.registerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/registerPage.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Label ID="registerPageLabel" runat="server" Text="Register Page"></asp:Label>
        <br />
        <asp:Label ID="messageLabel" runat="server" Text="" Visible="False" ></asp:Label>
        <br />
        <asp:TextBox class="textBox" ID="firstName" runat="server" placeholder="first name"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox class="textBox" ID="lastName" runat="server" placeholder="last name"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox class="textBox" ID="dob" runat="server" placeholder="mm/dd/yyyy" TextMode="Date"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:TextBox class="textBox" ID="phoneNumber" runat="server" placeholder="phone number" TextMode="Phone"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox class="textBox" ID="email" runat="server" placeholder="email" TextMode="Email"></asp:TextBox>
        <br />

        <br />
        <asp:Button ID="register" runat="server" Text="Register" OnClick="register_Click" />
        
        <br />
        <asp:Button ID="cancel" runat="server" Text="Cancel/Exit" OnClick="cancel_Click" style="height: 26px" />
        <br />


       
    </div>
</asp:Content>
