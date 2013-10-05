<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpDetails.aspx.cs" Inherits="Binding.EmpDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        
    
        <asp:DetailsView ID="grdEmployerView" runat="server" Height="50px" Width="125px">
        </asp:DetailsView>
        <asp:LinkButton ID="btnBack" runat="server" OnClick="btnBack_Click">Back</asp:LinkButton>
        
    
    </div>
    </form>
</body>
</html>
