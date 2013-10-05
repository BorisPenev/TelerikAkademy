using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.UI.WebControls;
using Musicstore.Server.Models;

namespace Musicstore.Client.WebApp
{
    public partial class Artists : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData.ddlSongsFill(lbSongs);
                LoadData.ddlAlbumsFill(lbAlbums, null);
                grdArtistsFill();
            }
        }

        private void grdArtistsFill()
        {
            var response = SessionState.Client.GetAsync("api/artist").Result;
            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content.ReadAsAsync<IEnumerable<Artist>>().Result;
                grdArtists.DataSource = albums;
                grdArtists.DataBind();
            }
        }

        protected void btnArtistSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtArtistName.Text))
            {
                var artist = new Artist(txtArtistName.Text)
                {
                    Country = txtArtistCountry.Text,
                    DateOfBirth = DateTime.Now
                };

                hfAlbums.Value.Split(',').ToList().ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var values = item.Split(';');
                        if (values.Count() > 1)
                        {
                            var album = new Album(Title = values[1])
                            {
                                Id = int.Parse(values[0])
                            };
                            artist.Albums.Add(album);
                        }
                    }
                });

                var response = new HttpResponseMessage();
                if (!string.IsNullOrEmpty(btnArtistSave.CommandArgument))
                {
                    artist.Id = int.Parse(btnArtistSave.CommandArgument);
                    response = SessionState.Client.PutAsJsonAsync("api/artist/" + artist.Id, artist).Result;
                }
                else
                {
                    response = SessionState.Client.PostAsJsonAsync("api/artist", artist).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsAsync<HttpResponseMessage>().Result;
                    grdArtistsFill();
                }

                ClearForm();
            }
        }

        protected void grdArtists_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearForm();

            var response = SessionState.Client.GetAsync("api/artist/"+ grdArtists.SelectedValue).Result;
            if (response.IsSuccessStatusCode)
            {
                var artist = response.Content.ReadAsAsync<Artist>().Result;
                btnArtistSave.CommandArgument = artist.Id.ToString();
                txtArtistName.Text = artist.Name;
                txtArtistCountry.Text = artist.Country;
                LoadData.ddlAlbumsFill(lbAlbums, () =>
                {
                    foreach (ListItem listItem in lbAlbums.Items)
                    {
                        if (artist.Albums.Any(a => a.Id == int.Parse(listItem.Value)))
                        {
                            listItem.Selected = true;
                            hfAlbums.Value += listItem.Value + ";" + listItem.Text + ",";
                        }
                        else
                        {
                            listItem.Selected = false;
                        }
                    }
                });
            }
        }

        protected void grdArtists_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var response = SessionState.Client.DeleteAsync("api/artist/" + e.Keys[0]).Result;
            if (response.IsSuccessStatusCode)
            {
                var a = response.Content.ReadAsAsync<HttpResponseMessage>().Result;
                grdArtistsFill();
            }
        }

        private void ClearForm()
        {
            btnArtistSave.CommandArgument = string.Empty;
            txtArtistName.Text = string.Empty;
            txtArtistCountry.Text = string.Empty;
            hfAlbums.Value = string.Empty;
            hfSongs.Value = string.Empty;
        }
    }
}