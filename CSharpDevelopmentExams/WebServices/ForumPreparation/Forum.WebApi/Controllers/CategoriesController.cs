using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using Forum.Models;
using Forum.Data;

namespace Forum.WebApi.Controllers
{
    public class CategoriesController : ApiController, IRequiresSessionState
    {
        private ForumContext db = new ForumContext();

        // GET api/Categories
        public IEnumerable<Category> GetCategories()
        {
            var sessionKey = GetSessionKey();
            //check user
            //if sessionKey is change call SaveSessionKey() before return
            return db.Categories.AsEnumerable();
        }

        private string GetSessionKey()
        {
            if (HttpContext.Current.Session["sessionKey"] != null)
            {
                return HttpContext.Current.Session["sessionKey"].ToString();
            }
         
            throw new InvalidOperationException("Session expire. Please login.");
        }

        protected void SaveSessionKey(string sessionKey)
        {
            HttpContext.Current.Session["sessionKey"] = sessionKey;
        }
    }
}