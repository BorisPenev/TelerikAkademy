<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MenuLinks.Index" %>

<%@ Register Src="~/LinkMenu.ascx" TagPrefix="userControl" TagName="LinkMenu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <userControl:LinkMenu runat="server" ID="LinkMenu" />
    </div>
    </form>
</body>
</html>
