<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="Binding.Employees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="grdEmployers" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnSelectedIndexChanged="grdEmployers_SelectedIndexChanged">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="EmpDetails.aspx?id={0}" DataTextField="FullName" DataTextFormatString="{0}" HeaderText="Exployers" />
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:FormView ID="grdFormView" runat="server" CellPadding="4" ForeColor="#333333" ItemType="Binding.Employee">
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text="<%#: Item.FirstName %>"></asp:Label>
            </ItemTemplate>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
        </asp:FormView>

        <asp:Repeater ID="rEmployers" runat="server" ItemType="Binding.Employee">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text="<%#: Item.FirstName %>"></asp:Label><br/>
            </ItemTemplate>
        </asp:Repeater>
        
        <br/>

        <asp:ListView ID="lvEmps" runat="server" ItemType="Binding.Employee">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text="<%#: Item.FirstName %>"></asp:Label><br/>
            </ItemTemplate>
        </asp:ListView>
        


    </div>
    </form>
</body>
</html>
