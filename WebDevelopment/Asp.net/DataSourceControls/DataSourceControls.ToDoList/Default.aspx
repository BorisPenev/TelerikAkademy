<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DataSourceControls.ToDoList.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:EntityDataSource ID="dsCategories" runat="server" ConnectionString="name=TodosEntities" DefaultContainerName="TodosEntities" EnableFlattening="False" EntitySetName="Categories"></asp:EntityDataSource>
    Category: <asp:DropDownList ID="ddlCategories" runat="server" DataSourceID="dsCategories" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList><br />
    
    <asp:GridView ID="grdTodos" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="dsTodos" OnRowEditing="grdTodos_RowEditing">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Body" HeaderText="Body" SortExpression="Body" />
            <asp:BoundField DataField="LastChangeDate" HeaderText="LastChangeDate" SortExpression="LastChangeDate" />
        </Columns>
    </asp:GridView>
    <asp:EntityDataSource ID="dsTodos" runat="server" 
        ConnectionString="name=TodosEntities" 
        DefaultContainerName="TodosEntities" 
        EnableDelete="True" 
        EnableFlattening="False" 
        EnableInsert="True" 
        EnableUpdate="True" 
        EntitySetName="Todoes" 
        Where="it.CategoryId = @CategoryId">
        <WhereParameters>
            <asp:ControlParameter ControlID="ddlCategories" Name="CategoryId" PropertyName="SelectedValue" Type="Int32" />
        </WhereParameters>
    </asp:EntityDataSource>
    Title: <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br/>
    Body: <asp:TextBox ID="txtBody" runat="server"></asp:TextBox><br/>
    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">Save</asp:LinkButton>
</asp:Content>
