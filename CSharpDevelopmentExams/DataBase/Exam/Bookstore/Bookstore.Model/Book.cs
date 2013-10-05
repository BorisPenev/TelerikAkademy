using System.Collections.Generic;

namespace Bookstore.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long? ISBN { get; set; }

        private ICollection<Author> _authors = new HashSet<Author>();
        public virtual ICollection<Author> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }
       
        public double? Price { get; set; }
        public string Url { get; set; }

        private ICollection<Review> _reviews = new HashSet<Review>();
        public virtual ICollection<Review> Reviews
        {
            get { return _reviews; }
            set { _reviews = value; }
        }
    }
}
