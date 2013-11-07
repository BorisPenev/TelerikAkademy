using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Exam.Models;

namespace Exam.Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        public static Expression<Func<Category, CategoryViewModel>> ToViewModel
        {
            get
            {
                return model => new CategoryViewModel
                {
                    Id = model.Id,
                    Name = model.Name
                };
            }
        }
    }
}