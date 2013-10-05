using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Musicstore.Server.Data
{
    public static class SessionState
    {
        //private static MusicstoreContext _db = new MusicstoreContext();
        //public static MusicstoreContext db
        //{
        //    get
        //    {
        //        return _db;
        //    }
        //}

        public static HttpClient Client { get; private set; }

        static SessionState()
        {
            Client = new HttpClient() { BaseAddress = new Uri(Settings.Default.WebApi) };
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
