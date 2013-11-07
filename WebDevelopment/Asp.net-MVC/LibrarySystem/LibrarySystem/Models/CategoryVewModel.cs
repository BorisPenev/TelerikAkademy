using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace LibrarySystem.Models
{
    public class CategoryVewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Category, CategoryVewModel>> ToViewModel
        {
            get
            {
                return model => new CategoryVewModel
                {
                    Id = model.Id,
                    Name = model.Name
                };
            }
        }
    }
}