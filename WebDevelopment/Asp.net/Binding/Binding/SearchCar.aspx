<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchCar.aspx.cs" Inherits="Binding.SearchCar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="ddlProducers" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProducers_SelectedIndexChanged"></asp:DropDownList>
        <asp:DropDownList ID="ddlModels" runat="server" ></asp:DropDownList>

        <asp:CheckBoxList ID="cbExtras" runat="server">
        </asp:CheckBoxList>
        <asp:RadioButtonList ID="rbEngine" runat="server">
            <asp:ListItem>Benzinov</asp:ListItem>
            <asp:ListItem>Dizelov</asp:ListItem>
            <asp:ListItem>El</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
        <br />
        <asp:Literal ID="lblResult" runat="server"></asp:Literal>

    </div>
    </form>
</body>
</html>
