using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Musicstore.Server.Models;

namespace Musicstore.Client.WebApp
{
    public static class SessionState
    {
        public static HttpClient Client { get; private set; }

        //private static List<Artist> artists = new List<Artist>();
        //public static List<Artist> Artists
        //{
        //    get { return artists; }
        //    set { artists = value; }
        //}

        static SessionState()
        {
            Client = new HttpClient() { BaseAddress = new Uri("http://localhost:9428/") };
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        
        
    }
}
