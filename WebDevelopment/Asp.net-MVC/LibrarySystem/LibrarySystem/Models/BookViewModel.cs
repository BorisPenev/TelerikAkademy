using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LibrarySystem.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(256)]
        public string Title { get; set; }

        [Required, StringLength(256)]
        public string Author { get; set; }

        [StringLength(256)]
        public string Isbn { get; set; }

        [StringLength(256)]
        public string WebSite { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string CategoryName { get; set; }

        [UIHint("_CategoryDropdown")]
        public virtual CategoryVewModel Category { get; set; }

        public static Expression<Func<Book, BookViewModel>> ToViewModel
        {
            get
            {
                return model => new BookViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Author = model.Author,
                    Category = new CategoryVewModel()
                    {
                        Id = model.Category.Id,
                        Name = model.Category.Name
                    },
                    CategoryName = model.Category.Name,
                    Description = model.Description,
                    Isbn = model.Isbn,
                    WebSite = model.WebSite
                };
            }
        }
    }
}