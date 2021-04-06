<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="surveyPage.aspx.cs" Inherits="AITR.surveyPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/surveyPage.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <asp:Label ID="questionLabel" runat="server" Text="question"></asp:Label>
        <asp:PlaceHolder ID="answerPlaceHolder" runat="server"></asp:PlaceHolder>

        <div class="buttonContainer">
            <asp:Button class="navButton" ID="cancelButton" runat="server" Text="Cancel" OnClick="cancelButton_Click" />
            <asp:Button class="navButton" ID="nextButton" runat="server" Text="Next" OnClick="nextButton_Click" />
        </div>


    </div>
</asp:Content>
