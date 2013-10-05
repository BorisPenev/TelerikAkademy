namespace Bookstore.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<BookstoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BookstoreContext context)
        {
            context.Database.ExecuteSqlCommand("IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Books_Title') CREATE INDEX IX_Books_Title ON Books (Title)");
            context.Database.ExecuteSqlCommand("IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Authors_Name') CREATE UNIQUE INDEX IX_Authors_Name ON Authors (Name)");
            context.Database.ExecuteSqlCommand("IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Books_ISBN') CREATE INDEX IX_Books_ISBN ON Books (ISBN)");
        }
    }
}
