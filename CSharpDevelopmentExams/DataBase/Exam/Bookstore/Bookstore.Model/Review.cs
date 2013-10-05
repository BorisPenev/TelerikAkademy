using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Model
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime CreatDate { get; set; }
        public string Text { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
