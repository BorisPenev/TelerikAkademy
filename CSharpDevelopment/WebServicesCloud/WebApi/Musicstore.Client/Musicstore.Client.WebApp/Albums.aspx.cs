using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.UI.WebControls;
using Musicstore.Server.Models;

namespace Musicstore.Client.WebApp
{
    public partial class Albums : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData.ddlArtistsFill(lbArtists, null);
                LoadData.ddlSongsFill(lbSongs);
                grdAlbumsFill();
            }
        }

        private void grdAlbumsFill()
        {
            var response = SessionState.Client.GetAsync("api/album").Result;
            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content.ReadAsAsync<IEnumerable<Album>>().Result;
                grdAlbums.DataSource = albums;
                grdAlbums.DataBind();
            }
        }

        protected void btnAlbumSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAlbumTitle.Text))
            {
                var album = new Album(txtAlbumTitle.Text);

                int year = 0;
                if (int.TryParse(txtAlbumYear.Text, out year))
                {
                    album.Year = year;
                }

                hfArtists.Value.Split(',').ToList().ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var values = item.Split(';');
                        if (values.Count() > 1)
                        {
                            var artist = new Artist(values[1])
                            {
                                Id = int.Parse(values[0])
                            };
                            album.Artists.Add(artist);
                        }
                    }
                });

                album.Producer = txtAlbumProducer.Text;
                
                var response = new HttpResponseMessage();
                if (!string.IsNullOrEmpty(btnAlbumSave.CommandArgument))
                {
                    album.Id = int.Parse(btnAlbumSave.CommandArgument);
                    response = SessionState.Client.PutAsJsonAsync("api/album/" + album.Id, album).Result;
                }
                else
                {
                    response = SessionState.Client.PostAsJsonAsync("api/album", album).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsAsync<HttpResponseMessage>().Result;
                    grdAlbumsFill();
                }
            }
            
            ClearForm();
        }

        protected void grdAlbums_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearForm();
            
            var response = SessionState.Client.GetAsync("api/album/"+ grdAlbums.SelectedValue).Result;
            if (response.IsSuccessStatusCode)
            {
                var album = response.Content.ReadAsAsync<Album>().Result;
                btnAlbumSave.CommandArgument = album.Id.ToString();
                txtAlbumTitle.Text = album.Title;
                txtAlbumYear.Text = album.Year.ToString();
                txtAlbumProducer.Text = album.Producer;
                LoadData.ddlArtistsFill(lbArtists, () =>
                {
                    foreach (ListItem listItem in lbArtists.Items)
                    {
                        if(album.Artists.Any(a => a.Id == int.Parse(listItem.Value)))
                        {
                            listItem.Selected = true;
                            hfArtists.Value += listItem.Value + ";" + listItem.Text + ",";
                        }
                        else
                        {
                            listItem.Selected = false;
                        }
                    }
                });
            }
        }

        protected void grdAlbums_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var response = SessionState.Client.DeleteAsync("api/album/" + e.Keys[0]).Result;
            if (response.IsSuccessStatusCode)
            {
                var a = response.Content.ReadAsAsync<HttpResponseMessage>().Result;
                grdAlbumsFill();
            }
        }

        private void ClearForm()
        {
            btnAlbumSave.CommandArgument = string.Empty;
            txtAlbumTitle.Text = string.Empty;
            txtAlbumYear.Text = string.Empty;
            txtAlbumProducer.Text = string.Empty;
            hfArtists.Value = string.Empty;
            hfSongs.Value = string.Empty;
        }
    }
}