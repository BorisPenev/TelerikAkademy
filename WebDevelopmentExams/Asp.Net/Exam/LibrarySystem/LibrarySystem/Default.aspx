<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LibrarySystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row">
        <div class="span1">
            <h1>Books</h1>
        </div>
        <div class="search-button">
            <div class="form-search">
                <div class="input-append">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="span3 search-query" placeholder="Search by book title / author..."></asp:TextBox>
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click">Search</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <asp:ListView ID="lvCategory" runat="server" SelectMethod="SelectCategories" ItemType="LibrarySystem.Models.Category" OnItemDataBound="lvCategory_ItemDataBound">
        <LayoutTemplate>
            <div class="row">
                <div id="itemPlaceholder" runat="server" class="span4"></div>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="span4">
                <h2><%#: Item.Name %></h2>
                <asp:Repeater ID="repeaterBooks" runat="server" ItemType="LibrarySystem.Models.Book">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <a href="/BookDetails?id=<%#: Item.Id %>">“<%#: Item.Title %>” <i>by <%#: Item.Author %></i></a>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Label runat="server" ID="lblNoRecords" Visible="False"><i>No books in this category.</i></asp:Label>

            </div>
        </ItemTemplate>
    </asp:ListView>

</asp:Content>
