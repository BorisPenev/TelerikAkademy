<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterStudents.aspx.cs" Inherits="AspNetControls.RegisterStudents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label runat="server" >First Name</asp:Label>
        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox><br/>
    
        <asp:Label runat="server">Last Name</asp:Label>
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox><br/>
        
        <asp:Label runat="server">Facility Number:</asp:Label>
        <asp:TextBox ID="txtFacilityNumber" runat="server"></asp:TextBox><br/>
        
        <asp:Label runat="server">University</asp:Label>
        <asp:DropDownList ID="ddlUniversity" runat="server"></asp:DropDownList><br/>
        
        <asp:Label runat="server">Courses</asp:Label>
        <asp:ListBox ID="lbCourse" runat="server"></asp:ListBox>
        
        <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">Save</asp:LinkButton>
    </div>
    </form>
</body>
</html>
