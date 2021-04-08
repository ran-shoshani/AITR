<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="staffPage.aspx.cs" Inherits="AITR.staffPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/staffPage.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
            <asp:Label ID ="staffPageLabel" runat="server" Text="Staff Page"></asp:Label>
            <br />
         <!-- all check boxes and the dropdown and the result gridview will be popolated from databasse  -->
            <asp:Label ID="Label1" runat="server" Text="Filters"></asp:Label>
            <br />
            <div class="filterContainer">
                <div class="filterRow">
                <asp:CheckBoxList ID="genderList" runat="server" >
                    
                </asp:CheckBoxList>
                 <asp:CheckBoxList ID="ageRangeList" runat="server">
                </asp:CheckBoxList>
                 <asp:CheckBoxList ID="banksList" runat="server">
                </asp:CheckBoxList>
                 <asp:CheckBoxList ID="bankServicesList" runat="server">
                </asp:CheckBoxList>
            </div>
            <br />
            <div class="filterRow">
                 <asp:CheckBoxList ID="magazinesList" runat="server">
                </asp:CheckBoxList>
                 <asp:CheckBoxList ID="magazinesSectionsList" runat="server">
                </asp:CheckBoxList>
                 <asp:CheckBoxList ID="sportList" runat="server">
                </asp:CheckBoxList>
                 <asp:CheckBoxList ID="travelDestinationsList" runat="server">
                </asp:CheckBoxList>
            </div>
            <br />
            <div class="filterRow">
                <asp:DropDownList ID="suburbList" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="postcodeList" runat="server">
                </asp:DropDownList>
                <asp:Button ID="clearFilterButton" runat="server" Text="Clear" />
                <asp:Button ID="searchWithFiltersButton" runat="server" Text="Search" />
                <br />
            </div>
            </div>
            <div class="searchContainer">
            <asp:Label ID="searchLable" runat="server" Text="Search"></asp:Label>
                <div class="searchRow">
                <asp:TextBox class="textBox" ID="firstName" runat="server" placeholder="first or last name"></asp:TextBox>
                <asp:TextBox class="textBox" ID="email" runat="server" placeholder="email"></asp:TextBox>
                <asp:Button ID="searchButton" runat="server" Text="Search" />

                </div>
            </div>
            <div class="searchResultsContainer">
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                <div>
                   <asp:Button ID="logoutButton" runat="server" Text="LogOut" />                   
                   <asp:Button ID="clearResultsButton" runat="server" Text="Clear Results" />

                </div>
            </div>
     </div>
</asp:Content>
