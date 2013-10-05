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

namespace Forum.WebApi.Controllers
{
    public class PostsController : ApiController
    {
        private ForumContext db = new ForumContext();

        // GET api/Posts
        public IEnumerable<Post> GetPosts()
        {
            var posts = db.Posts.Include(p => p.User);
            return posts.AsEnumerable();
        }

        // GET api/Posts/5
        public Post GetPost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return post;
        }

        // PUT api/Posts/5
        public HttpResponseMessage PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != post.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(post).State = EntityState.Modified;

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

        // POST api/Posts
        public HttpResponseMessage PostPost(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, post);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = post.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Posts/5
        public HttpResponseMessage DeletePost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Posts.Remove(post);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, post);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}