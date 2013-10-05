<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="LibrarySystem.Admin.Books" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>Edit Categories</h1>
    <asp:GridView ID="grdBooks" runat="server" ItemType="LibrarySystem.Models.Book"
        SelectMethod="SelectBooks" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="grdBooks_OnRowEditing"
        CssClass="gridview" AllowSorting="True" AllowPaging="True" PageSize="5" OnSelectedIndexChanged="grdBooks_OnSelectedIndexChanged" OnRowDataBound="grdBooks_RowDataBound" OnSorting="grdBooks_Sorting">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"/>
            <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author"/>
            <asp:BoundField DataField="Isbn" HeaderText="ISBN" SortExpression="Isbn" />
            <asp:HyperLinkField DataNavigateUrlFields="WebSite" DataTextField="WebSite" HeaderText="Web Site" SortExpression="WebSite" />
            <asp:BoundField DataField="Category.Name" HeaderText="Category" SortExpression="Category.Name" />
             <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <%#: Item.IsApproved %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" CommandName="Edit" runat="server" CssClass="link-button">Edit</asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" CommandName="Select" runat="server" CssClass="link-button">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    <div class="create-link">
        <asp:LinkButton runat="server" ID="btnCreateNew" CssClass="link-button" OnClick="btnCreateNew_Click">Create New</asp:LinkButton>
    </div>
    <asp:Panel runat="server" ID="pnlBooks" Visible="False" CssClass="panel">

        <h2 runat="server" id="lblHeader">Create New Book</h2>
        <label>
            <asp:Label runat="server" Text="Title:"></asp:Label>
            <asp:TextBox ID="txtTitle" runat="server" placeholder="Enter book title ..." MaxLength="256"></asp:TextBox>
        </label>

        <asp:Panel ID="pnlAdditionalInfo" runat="server" Visible="True">
            <label>
                <asp:Label runat="server" Text="Author(s):"></asp:Label>
                <asp:TextBox ID="txtAuthor" runat="server" placeholder="Enter book author / authors ..." MaxLength="256"></asp:TextBox>
            </label>

            <label>
                <asp:Label runat="server" Text="ISBN:"></asp:Label>
                <asp:TextBox ID="txtIsbn" runat="server" placeholder="Enter book ISBN ..." MaxLength="256"></asp:TextBox>
            </label>

            <label>
                <asp:Label runat="server" Text="Web site:"></asp:Label>
                <asp:TextBox ID="txtWebSite" runat="server" placeholder="Enter book web site ..." MaxLength="256"></asp:TextBox>
            </label>

            <label>
                <asp:Label runat="server" Text="Description:"></asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" placeholder="Enter book description ..." Height="160"></asp:TextBox>
            </label>

            <label>
                <asp:Label runat="server" Text="Category:"></asp:Label>
                <asp:DropDownList ID="ddlCategory" runat="server" SelectMethod="SelectCategories" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
            </label>
        </asp:Panel>

        <asp:LinkButton ID="btnSave" runat="server" CssClass="link-button" OnClick="btnSave_Click"></asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnCancel" runat="server" CssClass="link-button" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
    </asp:Panel>

    <div class="back-link">
        <a href="/">Back to books</a>
    </div>
</asp:Content>
