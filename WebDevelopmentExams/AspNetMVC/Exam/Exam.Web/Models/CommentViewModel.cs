using System;
using System.Linq;
using System.Linq.Expressions;
using Exam.Models;

namespace Exam.Web.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
    }
}