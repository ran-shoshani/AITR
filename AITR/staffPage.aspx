<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="staffPage.aspx.cs" Inherits="AITR.staffPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/staffPage.css" type="text/css" media="screen" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Label ID="staffPageLabel" runat="server" Text="Staff Page"></asp:Label>
        <br />
        <!-- all check boxes and the dropdown and the result gridview will be popolated from databasse  -->
        <asp:Label ID="Label1" runat="server" Text="Filters"></asp:Label>
        <br />
        <div class="filterContainer">
            <div class="filterRow">
                <div class="checkBoxContainer">
                    <asp:Label ID="Label2" runat="server" Text="Genders"></asp:Label>
                    <asp:CheckBoxList ID="genderList" runat="server">
                    </asp:CheckBoxList>
                </div>

                <div class="checkBoxContainer">
                    <asp:Label ID="Label3" runat="server" Text="Age"></asp:Label>
                    <asp:CheckBoxList ID="ageRangeList" runat="server">
                    </asp:CheckBoxList>
                </div>


                <div class="checkBoxContainer">
                    <asp:Label ID="Label4" runat="server" Text="Bank"></asp:Label>
                    <asp:CheckBoxList ID="banksList" runat="server">
                    </asp:CheckBoxList>
                </div>

                <div class="checkBoxContainer">
                    <asp:Label ID="Label5" runat="server" Text="Bank Services"></asp:Label>
                    <asp:CheckBoxList ID="bankServicesList" runat="server">
                    </asp:CheckBoxList>
                </div>
            </div>
            <br />
            <div class="filterRow">

                <div class="checkBoxContainer">
                    <asp:Label ID="Label6" runat="server" Text="Magazines"></asp:Label>
                    <asp:CheckBoxList ID="magazinesList" runat="server">
                    </asp:CheckBoxList>
                </div>
                <div class="checkBoxContainer">
                    <asp:Label ID="Label7" runat="server" Text="Magazine Sections"></asp:Label>
                    <asp:CheckBoxList ID="magazinesSectionsList" runat="server">
                    </asp:CheckBoxList>
                </div>
                <div class="checkBoxContainer">
                    <asp:Label ID="Label8" runat="server" Text="Sports"></asp:Label>
                    <asp:CheckBoxList ID="sportList" runat="server">
                    </asp:CheckBoxList>
                </div>
                <div class="checkBoxContainer">
                    <asp:Label ID="Label9" runat="server" Text="Travel Destinations"></asp:Label>
                    <asp:CheckBoxList ID="travelDestinationsList" runat="server">
                    </asp:CheckBoxList>
                </div>

            </div>
            <div class="filterRow">
                <asp:DropDownList ID="suburbList" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="postcodeList" runat="server">
                </asp:DropDownList>
                <asp:Button ID="dropDownsearchButton" runat="server" Text="DropDown Search" OnClick="dropDownsearchButton_Click" />
                <asp:Button ID="searchWithFiltersButton" runat="server" Text="CheckBox Search" OnClick="searchWithFiltersButton_Click" />
                <asp:Button ID="clearFilterButton" runat="server" Text="Clear" OnClick="clearFilterButton_Click" />

                <br />
            </div>
        </div>
        <div class="searchContainer">
            <asp:Label ID="searchLable" runat="server" Text="Search"></asp:Label>
            <div class="searchRow">
                <asp:TextBox class="textBox" ID="searchTextbox" runat="server" placeholder="name or email"></asp:TextBox>
                <asp:Button ID="textboxSearchButton" runat="server" Text="Search" OnClick="textboxSearchButton_Click" />

            </div>
        </div>
        <div class="searchResultsContainer">
            <asp:GridView ID="gridViewResults" runat="server"></asp:GridView>
            <div>
                <asp:Button ID="logoutButton" runat="server" Text="LogOut" OnClick="logoutButton_Click" />
                <asp:Button ID="clearResultsButton" runat="server" Text="Clear Results" OnClick="clearResultsButton_Click" />

            </div>
        </div>
    </div>
</asp:Content>
