using System;
using System.Linq.Expressions;

namespace LibrarySystem.Models
{
    public class SearchResultViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public static Expression<Func<Book, SearchResultViewModel>> ToViewModel
        {
            get
            {
                return model => new SearchResultViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Author = model.Author,
                    Category = model.Category.Name
                };
            }
        }
    }
}