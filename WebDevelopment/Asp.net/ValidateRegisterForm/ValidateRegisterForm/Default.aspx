<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ValidateRegisterForm.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="jquery" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>

        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:"
                 ValidationGroup="LogonInfo" ShowMessageBox="false" DisplayMode="BulletList" ShowSummary="true" ForeColor="Red" /> 
            UserName:
            <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="An UserName is required!" Text="*"
                ControlToValidate="txtUserName" ValidationGroup="LogonInfo" /><br />

            Password:
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="An Password is required!" Text="*"
                ControlToValidate="txtPassword" ValidationGroup="LogonInfo" /><br />

            Repeat Password:
            <asp:TextBox runat="server" ID="txtPassword2" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword2" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="An Repeat Password is required!" Text="*"
                ControlToValidate="txtPassword2" ValidationGroup="LogonInfo" /><br />
            
            <asp:LinkButton runat="server" ID="btnRegister" ValidationGroup="LogonInfo">Register</asp:LinkButton><br/>
            
             <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Following error occurs:"
                 ValidationGroup="PersonalInfo" ShowMessageBox="false" DisplayMode="BulletList" ShowSummary="true" ForeColor="Red" /> 

            First Name:
            <asp:TextBox runat="server" ID="txtFirstName"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="An First Name is required!" Text="*"
                ControlToValidate="txtFirstName" ValidationGroup="PersonalInfo" /><br />

            Last Name:
            <asp:TextBox runat="server" ID="txtLastName"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="An Last Name is required!" Text="*"
                ControlToValidate="txtLastName" ValidationGroup="PersonalInfo" /><br />

            Age:
            <asp:TextBox runat="server" ID="txtAge"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAge" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="An Age is required!" Text="*"
                ControlToValidate="txtAge" ValidationGroup="PersonalInfo" />
            <asp:RangeValidator ID="RangeValidatorAge" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="Age need to be between 18 and 81" Text="*"
                ControlToValidate="txtAge" ValidationGroup="PersonalInfo"
                 MaximumValue="81" MinimumValue="18" Type="Integer" /><br />

            Email:
            <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="An Email is required!" Text="*"
                ControlToValidate="txtEmail" ValidationGroup="PersonalInfo" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="Invalid Email Format" Text="*"
                ControlToValidate="txtEmail" ValidationGroup="PersonalInfo"
                 ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            <br />
            
            <asp:LinkButton runat="server" ID="LinkButton1" ValidationGroup="PersonalInfo">Register</asp:LinkButton><br/>
            
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" HeaderText="Following error occurs:"
                 ValidationGroup="AddressInfo" ShowMessageBox="false" DisplayMode="BulletList" ShowSummary="true" ForeColor="Red" /> 

            Address:
            <asp:TextBox runat="server" ID="txtAddress"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="An Address is required!" Text="*"
                ControlToValidate="txtAddress" ValidationGroup="AddressInfo" /><br />

            Phone:
            <asp:TextBox runat="server" ID="txtPhone"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="An Phone is required!" Text="*"
                ControlToValidate="txtPhone" ValidationGroup="AddressInfo" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone" runat="server"
                ForeColor="Red" Display="Dynamic" ErrorMessage="Invalid Phone Format" Text="*"
                ControlToValidate="txtPhone" ValidationGroup="AddressInfo" ValidationExpression="(\+|00)(359)\d{9}" />
            <br />

            <asp:TextBox ID="TextBoxValidationFix" runat="server" Text="dontremove" Visible="False" />
            <asp:CheckBox runat="server" ID="chkIAccept" Text="I Accept" />
            <asp:CustomValidator runat="server" ID="custCheck" ControlToValidate="TextBoxValidationFix" Text="*"
                OnServerValidate="checkCheckBox" ErrorMessage="Check CheckBox" /><br />

            <asp:LinkButton runat="server" ID="LinkButton2" ValidationGroup="AddressInfo">Register</asp:LinkButton><br/>
        </div>
    </form>
</body>
</html>
