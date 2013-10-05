<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="LibrarySystem.Admin.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Edit Categories</h1>
    <asp:GridView ID="grdCategories" runat="server" ItemType="LibrarySystem.Models.Category"
        SelectMethod="SelectCategories" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="grdCategories_RowEditing"
        CssClass="gridview" AllowSorting="True" AllowPaging="True" PageSize="5" OnSelectedIndexChanged="grdCategories_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Category Name" SortExpression="Id" />
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
    <asp:Panel runat="server" ID="pnlCategory" Visible="False" CssClass="panel">
        <h2 runat="server" id="lblCategoryHeader">Create New Category</h2>
        
        <label>
            <asp:Label runat="server" Text="Category:"></asp:Label>
            <asp:TextBox ID="txtCategoryName" runat="server" placeholder="Enter category name..." MaxLength="256"></asp:TextBox>
        </label>
        <br />

        <asp:LinkButton ID="btnSave" runat="server" CssClass="link-button" OnClick="btnSave_Click"></asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnCancel" runat="server" CssClass="link-button" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
    </asp:Panel>
    
    <div class="back-link">
        <a href="/">Back to books</a>
    </div>
</asp:Content>
