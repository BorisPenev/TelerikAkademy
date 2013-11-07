using System.Data.Entity;
using LibrarySystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibrarySystem.Data
{
    public class ApplicationDbContext : IdentityDbContextWithCustomUser<ApplicationUser>
    {
        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Book> Books { get; set; }
    }
}