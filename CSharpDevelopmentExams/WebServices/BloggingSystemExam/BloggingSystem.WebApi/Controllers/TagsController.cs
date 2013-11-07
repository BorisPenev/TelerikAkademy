using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using BloggingSystem.Data;
using BloggingSystem.Models;
using BloggingSystem.WebApi.Models;

namespace BloggingSystem.WebApi.Controllers
{
    public class TagsController : BaseApiController
    {
        //GET api/tags
        public IQueryable<TagModel> GetAll(string sessionKey)
        {
            var context = new BlogContext();
            var responseMsg = this.PerformOperationAndHandleExceptionsWithSessionKey(sessionKey, context, () =>
            {
                var tagsModel = context.Tags.Select(p => new TagModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Posts = p.Posts.Count()
                });
                return tagsModel.OrderBy(p => p.Name);
            });

            return responseMsg;
        }

        //GET api/tags/{tagId}/posts
        public FullTagModel GetPostsByTagId(int tagId, string sessionKey)
        {
            var context = new BlogContext();
            var responseMsg = this.PerformOperationAndHandleExceptionsWithSessionKey(sessionKey, context, () =>
            {
                if (tagId < 1)
                {
                    throw new InvalidOperationException("Invalid tagId");
                }

                var tag = context.Tags.FirstOrDefault(p => p.Id == tagId);
                if (tag != null)
                {
                    var fullTagModel = new FullTagModel()
                    {
                        Name = tag.Name,
                        Id = tag.Id,
                        PostsModels = tag.Posts.OrderByDescending(p => p.PostDate).Select(p => new PostModel()
                        {
                            Id = p.Id,
                            Title = p.Title,
                            PostedBy = p.User.DisplayName,
                            PostDate = p.PostDate,
                            Text = p.Text,
                            Comments = p.Comments.Select(c => new CommentModel()
                            {
                                Text = c.Text,
                                CommentedBy = c.User.DisplayName,
                                PostDate = c.PostDate
                            })
                        })
                    };
                    return fullTagModel;
                }
                else
                {
                    throw new InvalidOperationException("Tag with id " + tagId + " do not exist");
                }
            });

            return responseMsg;
        }
    }
}
