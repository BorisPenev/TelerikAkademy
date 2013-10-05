using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Model;

namespace Bookstore.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext()
            : base("Bookstore")
        {
            
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(x => x.Id);
            modelBuilder.Entity<Book>().Property(x => x.Title).IsRequired();
            modelBuilder.Entity<Book>().Property(x => x.Title).HasMaxLength(256);
            modelBuilder.Entity<Book>().Property(x => x.Price).IsOptional();

            modelBuilder.Entity<Author>().HasKey(x => x.Id);
            modelBuilder.Entity<Author>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Author>().Property(x => x.Name).IsUnicode(true);
            modelBuilder.Entity<Author>().Property(x => x.Name).HasMaxLength(256);

            modelBuilder.Entity<Review>().HasKey(x => x.Id);
            modelBuilder.Entity<Review>().Property(x => x.CreatDate).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
