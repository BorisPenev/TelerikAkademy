<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="08_FindAllProducts.aspx.cs" Inherits="ADONET.WebApp.FindAllProducts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Find Products <asp:TextBox ID="txtSearchOption" runat="server"></asp:TextBox> <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click">Search</asp:LinkButton>
            <asp:GridView ID="grdResult" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
