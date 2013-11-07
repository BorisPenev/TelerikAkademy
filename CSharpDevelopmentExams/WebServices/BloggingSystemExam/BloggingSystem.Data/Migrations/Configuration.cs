namespace BloggingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<BloggingSystem.Data.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BloggingSystem.Data.BlogContext context)
        {
            context.Database.ExecuteSqlCommand("IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Users_Username') CREATE UNIQUE INDEX IX_Users_Username ON Users (Username)");
            context.Database.ExecuteSqlCommand("IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Users_DisplayName') CREATE UNIQUE INDEX IX_Users_DisplayName ON Users (DisplayName)");
            context.Database.ExecuteSqlCommand("IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Tags_Name') CREATE UNIQUE INDEX IX_Tags_Name ON Tags (Name)");


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
