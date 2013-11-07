namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Movies.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Movies.Models.ApplicationDbContext context)
        {
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

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                context.Roles.AddOrUpdate(new Role("Admin"));
            }
            context.SaveChanges();
        }
    }
}
