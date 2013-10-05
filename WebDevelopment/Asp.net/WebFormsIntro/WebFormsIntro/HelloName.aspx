<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelloName.aspx.cs" Inherits="WebFormsIntro.HelloName" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
        <br />
        <asp:Label ID="lblResult" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
