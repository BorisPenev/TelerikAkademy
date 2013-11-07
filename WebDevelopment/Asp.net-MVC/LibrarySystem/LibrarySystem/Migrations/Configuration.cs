using System.Collections.ObjectModel;
using LibrarySystem.Data;
using LibrarySystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibrarySystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedUserAdmin(context);
        }

        private static void SeedUserAdmin(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var db = new ApplicationDbContext();

            var userAdmin = new ApplicationUser()
            {
                UserName = "admin",
                Country = "adminLandia",
                Logins = new Collection<UserLogin>()
                {
                    new UserLogin()
                    {
                        LoginProvider = "Local",
                        ProviderKey = "admin",
                    }
                },
                Roles = new Collection<UserRole>()
                {
                    new UserRole()
                    {
                        Role = new Role("Admin")
                    }
                }
            };

            db.Users.Add(userAdmin);
            db.UserSecrets.Add(new UserSecret("admin",
                "ACQbq83L/rsvlWq11Zor2jVtz2KAMcHNd6x1SN2EXHs7VuZPGaE8DhhnvtyO10Nf5Q=="));//admin123
            db.SaveChanges();
        }
    }
}
