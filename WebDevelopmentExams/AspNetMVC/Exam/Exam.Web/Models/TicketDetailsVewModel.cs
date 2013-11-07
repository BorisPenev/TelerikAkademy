using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Exam.Models;

namespace Exam.Web.Models
{
    public class TicketDetailsVewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public PriorityType Priority { get; set; }

        public string AuthorName { get; set; }

        public string ScreenshotUrl { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public static Expression<Func<Ticket, TicketDetailsVewModel>> ToViewModel
        {
            get
            {
                return model => new TicketDetailsVewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Priority = model.Priority,
                    AuthorName = model.Author.UserName,
                    ScreenshotUrl = model.ScreenshotURL,
                    CategoryName = model.Category.Name,
                    Comments = model.Comments.Select(x => new CommentViewModel()
                    {
                        Id = x.Id,
                        AuthorName = x.Author.UserName,
                        Content = x.Content
                    })
                };
            }
        }
    }
}