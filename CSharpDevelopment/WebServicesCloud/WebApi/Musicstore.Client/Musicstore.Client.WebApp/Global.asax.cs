using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Musicstore.Server.Models;

namespace Musicstore.Client.WebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //if (SessionState.Artists.Count == 0)
            //{
            //    var response = SessionState.Client.GetAsync("api/artist").Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var artists = response.Content.ReadAsAsync<IEnumerable<Artist>>().Result.ToList();
            //        SessionState.Artists.AddRange(artists);
            //    }
            //}
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}