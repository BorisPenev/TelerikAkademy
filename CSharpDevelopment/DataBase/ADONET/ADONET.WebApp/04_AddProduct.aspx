<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_AddProduct.aspx.cs" Inherits="ADONET.WebApp.AddProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Product Name <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox> <asp:LinkButton ID="btnInsert" runat="server" OnClick="btnInsert_Click">Insert</asp:LinkButton>
            <asp:GridView ID="grdResult" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
