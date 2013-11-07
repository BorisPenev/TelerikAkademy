using System.Collections.ObjectModel;
using Exam.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Exam.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedUserAdmin(context);

            SeedInitData(context);
        }

        private static void SeedInitData(ApplicationDbContext context)
        {
            if (context.Tickets.Any())
            {
                return;
            }

            var rand = new Random();

            var user = new ApplicationUser
            {
                UserName = "TestUser"
            };

            for (int i = 0; i < 10; i++)
            {
                var category = new Category
                {
                    Name = "Category " + i,
                };

                var ticketCount = rand.Next(0, 10);
                for (int j = 0; j < ticketCount; j++)
                {
                    var ticket = new Ticket();
                    ticket.AuthorId = user.Id;
                    //ticket.CategoryId = category.Id;
                    ticket.Description = "description: " + i;
                    ticket.Title = "ticket " + i;
                    ticket.ScreenshotURL = "/Content/report.png";

                    var commentsCount = rand.Next(0, 10);
                    for (int k = 0; k < commentsCount; k++)
                    {
                        ticket.Comments.Add(new Comment
                        {
                            Content = "Mnou qk ticket brat.", Author = user
                        });
                    }
                    category.Tickets.Add(ticket);
                    context.Tickets.Add(ticket);
                }
                context.Categories.Add(category);
            }
            context.SaveChanges();
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
