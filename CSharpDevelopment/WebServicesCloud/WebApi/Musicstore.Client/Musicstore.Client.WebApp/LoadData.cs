using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI.WebControls;
using Musicstore.Server.Models;

namespace Musicstore.Client.WebApp
{
    public static class LoadData
    {
        public static void ddlSongsFill(ListBox ddlSongs)
        {
            var response = SessionState.Client.GetAsync("api/song").Result;
            if (response.IsSuccessStatusCode)
            {
                var songs = response.Content.ReadAsAsync<IEnumerable<Song>>().Result;
                ddlSongs.DataTextField = "Title";
                ddlSongs.DataValueField = "Id";
                ddlSongs.DataSource = songs.ToList();
                ddlSongs.DataBind();
            }
        }

        public static void ddlArtistsFill(ListBox ddlArtists, Action action)
        {
            var response = SessionState.Client.GetAsync("api/artist").Result;
            if (response.IsSuccessStatusCode)
            {
                var artists = response.Content.ReadAsAsync<IEnumerable<Artist>>().Result.ToList();
                ddlArtists.DataTextField = "Name";
                ddlArtists.DataValueField = "Id";
                ddlArtists.DataSource = artists.ToList();
                ddlArtists.DataBind();

                if (action != null)
                {
                    action();
                }
            }
        }

        public static void ddlAlbumsFill(ListBox ddlAlbums, Action action)
        {
            var response = SessionState.Client.GetAsync("api/album").Result;
            if (response.IsSuccessStatusCode)
            {
                var artists = response.Content.ReadAsAsync<IEnumerable<Album>>().Result;
                ddlAlbums.DataTextField = "Title";
                ddlAlbums.DataValueField = "Id";
                ddlAlbums.DataSource = artists.ToList();
                ddlAlbums.DataBind();

                if (action != null)
                {
                    action();
                }
            }
        }
    }
}