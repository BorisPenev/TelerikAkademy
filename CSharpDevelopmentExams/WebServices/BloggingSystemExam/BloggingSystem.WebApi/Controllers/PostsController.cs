using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using BloggingSystem.Data;
using BloggingSystem.Models;
using BloggingSystem.WebApi.Models;
using WebGrease.Css.Extensions;

namespace BloggingSystem.WebApi.Controllers
{
    public class PostsController : BaseApiController
    {
        //GET api/posts
        public IQueryable<PostModel> GetAll(string sessionKey)
        {
            var context = new BlogContext();
            var responseMsg = this.PerformOperationAndHandleExceptionsWithSessionKey(sessionKey, context, () =>
            {
                var postsModel = context.Posts.Select(p => new PostModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    PostedBy = p.User.DisplayName,
                    PostDate = p.PostDate,
                    Text = p.Text,
                    Tags = p.Tags.Select(t => t.Name),
                    Comments = p.Comments.Select(c => new CommentModel()
                    {
                        Text = c.Text,
                        CommentedBy = c.User.DisplayName,
                        PostDate = c.PostDate
                    })
                });
                return postsModel.OrderByDescending(p => p.PostDate);
            });

            return responseMsg;
        }

        //Get api/posts?page=P&count=C
        public IQueryable<PostModel> GetAllPaged(int page, int count, string sessionKey)
        {
            var models = this.GetAll(sessionKey)
                .Skip(page * count)
                .Take(count);
            return models;
        }

        //POST api/posts/create
        public HttpResponseMessage PostCreate(PostModel post, string sessionKey)
        {
            var context = new BlogContext();
            var responseMsg = this.PerformOperationAndHandleExceptionsWithSessionKey(sessionKey, context, () =>
            {
                HttpResponseMessage response;
                var newPost = new Post();
                using (var tran = new TransactionScope())
                {
                    newPost.Title = post.Title;
                    newPost.Text = post.Text;
                    newPost.PostDate = DateTime.Now;
                    newPost.User = context.Users.FirstOrDefault(u => u.SessionKey == sessionKey);

                    var tags = Helpers.GetTags(post, context);
                    tags.ForEach(t => newPost.Tags.Add(t));
                
                    context.Posts.Add(newPost);
                    context.SaveChanges();
                    tran.Complete();
                }

                if (newPost.Id > 0)
                {
                    response = this.Request.CreateResponse(HttpStatusCode.Created, new ResponsePostModel()
                    {
                        Id = newPost.Id,
                        Title = newPost.Title
                    });
                }
                else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                }

                return response;
            });

            return responseMsg;
        }

        //Get api/posts?keyword=webapi
        public IQueryable<PostModel> GetAllByKeywords(string keyword, string sessionKey)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var models = this.GetAll(sessionKey)
                    .Where(p => p.Title.Contains(keyword))
                    .OrderByDescending(p => p.PostDate);

                return models;
            }
            return new List<PostModel>().AsQueryable();
        }

        //Get api/posts?tags=web,webapi
        public IQueryable<PostModel> GetAllByTags(string tags, string sessionKey)
        {
            if (!string.IsNullOrEmpty(tags))
            {
                var tagss = tags.Split(',').ToList();
                var models = this.GetAll(sessionKey)
                    .Where(p => p.Tags.Intersect(tagss).Any())
                    .OrderByDescending(p => p.PostDate);

                return models;
            }
            return new List<PostModel>().AsQueryable();
        }

        //PUT api/posts/{postId}/comment
        public HttpResponseMessage PutComment(int postId, CommentModel comment, string sessionKey)
        {
            var context = new BlogContext();
            var responseMsg = this.PerformOperationAndHandleExceptionsWithSessionKey(sessionKey, context, () =>
            {
                if (comment != null && string.IsNullOrEmpty(comment.Text))
                {
                    throw new InvalidOperationException("Comment text cannot be empty");
                }

                var post = context.Posts.FirstOrDefault(p => p.Id == postId);
                if (post != null)
                {
                    post.Comments.Add(new Comment()
                    {
                        Text = comment.Text,
                        User = context.Users.FirstOrDefault(u => u.SessionKey == sessionKey),
                        PostDate = DateTime.Now
                    });
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Post with id " + postId + " do not exist");
                }

                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                return response;
            });

            return responseMsg;
        }
    }
}
