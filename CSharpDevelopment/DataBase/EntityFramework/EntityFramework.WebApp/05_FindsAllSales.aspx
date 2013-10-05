<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_FindsAllSales.aspx.cs" Inherits="EntityFramework.WebApp.FindsAllSales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Select Region<asp:DropDownList ID="ddlRegion" runat="server"></asp:DropDownList><br/>
            Select Date Range:<br/>
            Start Date <asp:Calendar ID="txtStartDate" runat="server"></asp:Calendar><br/>
            End Date <asp:Calendar ID="txtEndDate" runat="server"></asp:Calendar><br/>
            <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click">Search</asp:LinkButton><br/>
            <asp:GridView ID="grdResult" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
