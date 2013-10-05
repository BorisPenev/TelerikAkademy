using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models.Data
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

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}