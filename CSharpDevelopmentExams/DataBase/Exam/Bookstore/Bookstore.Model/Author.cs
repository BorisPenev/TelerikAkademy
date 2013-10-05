using System.Collections.Generic;

namespace Bookstore.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private ICollection<Book> _books = new HashSet<Book>();
        public virtual ICollection<Book> Books
        {
            get { return _books; }
            set { _books = value; }
        }
    }
}
