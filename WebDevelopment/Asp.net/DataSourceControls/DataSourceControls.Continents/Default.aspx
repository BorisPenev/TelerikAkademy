<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DataSourceControls.Continents.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
            <legend>Continents</legend>
            <asp:EntityDataSource ID="dsContinents" runat="server" ConnectionString="name=ContinentsEntities" DefaultContainerName="ContinentsEntities" EnableFlattening="False" EntitySetName="Continents">
            </asp:EntityDataSource>
            <asp:ListBox ID="lbContinents" runat="server" DataSourceID="dsContinents" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:ListBox>
            <br />
            <asp:Label ID="lbl" runat="server" Text="Add New Continent"></asp:Label>
            <asp:TextBox ID="txtContinent" runat="server"></asp:TextBox>
            <br />
            <asp:LinkButton ID="btnSaveContinent" runat="server" OnClick="btnSaveContinent_Click">Save Continent</asp:LinkButton>
        </fieldset>

        <asp:EntityDataSource ID="dsCountries" runat="server"
            ConnectionString="name=ContinentsEntities"
            DefaultContainerName="ContinentsEntities"
            EntitySetName="Countries"
            Where="it.ContinentId = @ContinentId" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True">
            <WhereParameters>
                <asp:ControlParameter ControlID="lbContinents" Name="ContinentId" PropertyName="SelectedValue" Type="Int32" />
            </WhereParameters>
        </asp:EntityDataSource>

        <asp:GridView ID="grdCountries" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="dsCountries" ForeColor="#333333" GridLines="None" OnRowEditing="grdCountries_RowEditing">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Language" HeaderText="Language" SortExpression="Language" />
                <asp:ImageField DataImageUrlField="Id" DataImageUrlFormatString="~/ImageHttpHandler.ashx?id={0}" HeaderText="Flag">
                    <ControlStyle Height="120px" Width="120px" />
                </asp:ImageField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <br />
        Country Name:
        <asp:TextBox ID="txtCountryName" runat="server"></asp:TextBox><br />
        Country Language:
        <asp:TextBox ID="txtCountryLanguage" runat="server"></asp:TextBox><br />
        Country Flag:
        <asp:FileUpload ID="fuPicture" runat="server" />
        <br />
        <asp:LinkButton ID="btnSaveCountry" runat="server" OnClick="btnSaveCountry_Click">Save Country</asp:LinkButton>
        <br />
        <fieldset>
            <legend>Cities</legend>
            <asp:EntityDataSource ID="dsCities" runat="server" ConnectionString="name=ContinentsEntities" DefaultContainerName="ContinentsEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="Cities" Where="it.CountryID = @CountryId">
                <WhereParameters>
                    <asp:ControlParameter ControlID="grdCountries" Name="CountryId" PropertyName="SelectedValue" Type="Int32" />
                </WhereParameters>
                <InsertParameters>
                    <asp:ControlParameter Name="CountryId" ControlId="grdCountries" Type="Int32" />
                </InsertParameters>
            </asp:EntityDataSource>
            <asp:ListView ID="lvCities" runat="server" DataKeyNames="Id" DataSourceID="dsCities" InsertItemPosition="LastItem">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFFFFF;color: #284775;">
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="PopulationLabel" runat="server" Text='<%# Eval("Population") %>' />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr style="background-color: #999999;">
                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="PopulationTextBox" runat="server" Text='<%# Bind("Population") %>' />
                        </td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="PopulationTextBox" runat="server" Text='<%# Bind("Population") %>' />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: #E0FFFF;color: #333333;">
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="PopulationLabel" runat="server" Text='<%# Eval("Population") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                    <tr runat="server" style="background-color: #E0FFFF;color: #333333;">
                                        <th runat="server">Action</th>
                                        <th runat="server">Name</th>
                                        <th runat="server">Population</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF">
                                <asp:DataPager ID="DataPager1" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #E2DED6;font-weight: bold;color: #333333;">
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="PopulationLabel" runat="server" Text='<%# Eval("Population") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </fieldset>
    </form>
</body>
</html>
