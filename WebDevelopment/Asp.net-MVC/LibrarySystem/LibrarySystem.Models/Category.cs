using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(256)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Category()
        {
            Books = new HashSet<Book>();
        }
    }
}