<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="startPage.aspx.cs" Inherits="AITR.startPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/startPage.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="startSurveyContainer">
            <asp:Label ID ="welcomeSurveyLabel" runat="server" Text="Welcome To AIT Survey"></asp:Label>
            <asp:Button ID="startSurveyButton" runat="server" Text="Start" OnClick="startSurveyButton_Click"/>
        </div>


        <div class="loginContainer">
            <asp:Label ID="staffLoginLabel" runat="server" Text="Staff Login"></asp:Label>
            <asp:TextBox ID="userNameTextBox" runat="server"></asp:TextBox>
            <asp:TextBox ID="userPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click" />
        </div>
    </div>
</asp:Content>
