using LibrarySystem.Models;

namespace LibrarySystem.Data
{
    public interface IUowData
    {
        IRepository<Category> Categories { get; }

        IRepository<Book> Books { get; }

        int SaveChanges();
    }
}
