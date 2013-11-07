using System;
using System.Linq.Expressions;
using Exam.Models;

namespace Exam.Web.Models
{
    public class TopTicketsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }


        public string AuthorName { get; set; }


        public int CommentsCount { get; set; }

        public static Expression<Func<Ticket, TopTicketsViewModel>> ToViewModel
        {
            get
            {
                return model => new TopTicketsViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    CategoryName = model.Category.Name,
                    AuthorName = model.Author.UserName,
                    CommentsCount = model.Comments.Count
                };
            }
        }
    }
}