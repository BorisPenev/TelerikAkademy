using System.Data.Entity;
using Bookstore.Model;

namespace Bookstore.Data
{
    public class LogsContext : DbContext
    {
        public LogsContext()
            : base("Logs")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<BookstoreContext>());
        }

        public DbSet<SearchLogs> SearchLogs { get; set; }
    }
}
