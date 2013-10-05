<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="11_UsersInGroups.aspx.cs" Inherits="EntityFramework.WebApp.UsersInGroups" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Add User In Administrator Group:
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click">Add</asp:LinkButton><br />
            <asp:GridView ID="grdResult" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
