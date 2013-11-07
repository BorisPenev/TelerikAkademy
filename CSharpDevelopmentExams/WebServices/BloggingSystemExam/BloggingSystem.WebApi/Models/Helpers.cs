using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BloggingSystem.Data;
using BloggingSystem.Models;
using WebGrease.Css.Extensions;

namespace BloggingSystem.WebApi.Models
{
    class Helpers
    {
        public static IEnumerable<Tag> GetTags(PostModel post, BlogContext context)
        {
            var result = new List<Tag>();

            if (post != null)
            {
                post.Tags.ForEach(pt => GetTag(context, pt, result));
                var matches = Regex.Matches(post.Title, @"\w+");
                matches.Cast<Match>().Select(m => m.Value).ForEach(pt => GetTag(context, pt, result));
            }

            return result;
        }

        private static void GetTag(BlogContext context, string pt, List<Tag> result)
        {
            if (!string.IsNullOrEmpty(pt))
            {
                var tag = context.Tags.FirstOrDefault(t => t.Name == pt);
                if (tag == null)
                {
                    tag = new Tag()
                    {
                        Name = pt.ToLower()
                    };
                    context.Tags.Add(tag);
                    context.SaveChanges();
                }
                result.Add(tag);
            }
        }
    }
}
