<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Artists.aspx.cs" Inherits="Musicstore.Client.WebApp.Artists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            var defaultOptions = {
                moveEffect: 'blind',
                moveEffectOptions: { direction: 'vertical' },
                moveEffectSpeed: 'fast',
            };

            var widgets = {
                '#<%= lbAlbums.ClientID %>': $.extend($.extend({}, defaultOptions), {
                    sortMethod: 'standard',
                    sortable: true
                }),
                '#<%= lbSongs.ClientID %>': $.extend($.extend({}, defaultOptions), {
                    sortMethod: 'standard',
                    sortable: true
                })
            };

            $.each(widgets, function (id, i) {
                $(id).multiselect(i).on('multiselectChange', function (evt, ui) {
                    var values = "";
                    var find = $(id).find("option:selected");
                    for (var i = 0; i < find.length; i++) {
                        if (find[i].selected) {
                            values += find[i].value + ";" + find[i].text + ",";
                        }
                    }

                    if (id == '#<%= lbAlbums.ClientID %>') {
                        $('#<%= hfAlbums.ClientID %>').val(values);
                    } else {
                        $('#<%= hfSongs.ClientID %>').val(values);
                    }
                });
            });
        });
        </script>
    <link type="text/css" rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.10/themes/cupertino/jquery-ui.css">
    <style type="text/css">
        .multiselect {
            width: 450px;
            height: 200px;
        }

        .ui-widget {
            font-family: Lucida Grande, Lucida Sans, Arial, sans-serif;
            font-size: 0.9em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <fieldset>
            <legend>Artist:</legend>
            Name:
                <asp:TextBox ID="txtArtistName" runat="server"></asp:TextBox><br />
            Country:
                <asp:TextBox ID="txtArtistCountry" runat="server"></asp:TextBox><br />
            <fieldset style="display: inline-block">
                <legend>Albums:</legend>
                <asp:ListBox ID="lbAlbums" runat="server" CssClass="multiselect"></asp:ListBox>
                <asp:HiddenField ID="hfAlbums" runat="server" />
            </fieldset>
            <fieldset style="display: inline-block">
                <legend>Songs:</legend>
                <asp:ListBox ID="lbSongs" runat="server" CssClass="multiselect"></asp:ListBox>
                <asp:HiddenField ID="hfSongs" runat="server" />
            </fieldset>
            <br />
            <asp:LinkButton ID="btnArtistSave" runat="server" OnClick="btnArtistSave_Click">Save</asp:LinkButton>
        </fieldset>
        <asp:GridView ID="grdArtists" runat="server" DataKeyNames="Id" OnRowDeleting="grdArtists_RowDeleting" OnSelectedIndexChanged="grdArtists_SelectedIndexChanged">
            <Columns>
                <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
