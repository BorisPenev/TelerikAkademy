using System;
using System.Linq.Expressions;
using Exam.Models;

namespace Exam.Web.Models
{
    public class TicketsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }


        public string AuthorName { get; set; }


        public PriorityType Priority { get; set; }

        public string PriorityAsString
        {
            get
            {
                return Priority.ToString();
            }
        }

        public static Expression<Func<Ticket, TicketsViewModel>> ToViewModel
        {
            get
            {
                return model => new TicketsViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    CategoryId = model.Category.Id,
                    CategoryName = model.Category.Name,
                    AuthorName = model.Author.UserName,
                    Priority = model.Priority
                };
            }
        }
    }
}