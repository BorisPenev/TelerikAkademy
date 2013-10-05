<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Escaping.aspx.cs" Inherits="AspNetControls.Escaping" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="txtSend" runat="server"></asp:TextBox>
        <asp:LinkButton ID="btnSend" runat="server" OnClick="btnSend_Click">Send Text</asp:LinkButton>
        <br />
        <asp:TextBox ID="txtRead" runat="server"></asp:TextBox>
        <asp:Label ID="lblRead" runat="server" Text="Label"></asp:Label>
    
    </div>
    </form>
</body>
</html>
