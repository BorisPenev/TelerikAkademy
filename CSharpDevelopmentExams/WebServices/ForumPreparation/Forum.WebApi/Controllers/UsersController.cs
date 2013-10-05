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
using Forum.Models;
using Forum.Data;
using Forum.WebApi.Models;

namespace Forum.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        public int MinUsernameLength = 6;
        public int MaxUsernameLength = 30;
        public string UsernameVlidCharachters = "";

        private ForumContext db = new ForumContext();
        private IDbContextFactory<DbContext> _contextFactory;

        public UsersController()
        {
            this._contextFactory = new ForumDbContextFactory();
        }

        public UsersController(IDbContextFactory<DbContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }

        public HttpResponseMessage PostRegisterUser(UserModel model)
        {
            //using (var context = _contextFactory.Create())
            //{
                
            //}
            using (db)
            {
                this.ValidateUsername(model.Username);
                this.ValidateNickname(model.Nickname);
                this.ValidateAuthCode(model.AuthCode);
            }
            return new HttpResponseMessage();
        }

        private void ValidateAuthCode(string authCode)
        {
            if (authCode == null || authCode.Length != Sha1Length)
            {
                throw new ArgumentNullException();
            }
        }

        private void ValidateNickname(string nickname)
        {
            throw new NotImplementedException();
        }

        private void ValidateUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username cannot be null");
            }

            if(username.Length < MinUsernameLength && username.Length > MaxUsernameLength)
            {
                throw new ArgumentOutOfRangeException(string.Format("username must be between {0} and {1}", MinUsernameLength, MaxUsernameLength)); 
            }
            if(username.Any(ch => !UsernameVlidCharachters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException("username must be contains only latin letters, digits, .,_"); 
            }
        }


        public HttpResponseMessage PostLoginUser()
        {
            return new HttpResponseMessage();
        }

        public HttpResponseMessage PutLogoutUser()
        {
            return new HttpResponseMessage();
        }

        // GET api/Users
        public IEnumerable<User> GetUsers()
        {
            return db.Users.AsEnumerable();
        }

        // GET api/Users/5
        public User GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return user;
        }

        // PUT api/Users/5
        public HttpResponseMessage PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != user.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Users
        public HttpResponseMessage PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Users/5
        public HttpResponseMessage DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Users.Remove(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}