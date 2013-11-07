using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class Book
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
        public string Description { get; set; }

        [UIHint("_CategoryDropdown")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}