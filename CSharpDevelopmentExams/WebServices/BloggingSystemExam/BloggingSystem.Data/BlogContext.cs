using System.Data.Entity;
using BloggingSystem.Models;

namespace BloggingSystem.Data
{
    public class BlogContext: DbContext
    {
        public BlogContext() : base("BlogConnectionString")
        {
            
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasKey(x => x.Id);
            modelBuilder.Entity<Comment>().Property(x => x.Text).IsRequired();
            modelBuilder.Entity<Comment>().Property(x => x.Text).HasMaxLength(256);

            modelBuilder.Entity<Post>().HasKey(x => x.Id);
            modelBuilder.Entity<Post>().Property(x => x.Title).IsRequired();
            modelBuilder.Entity<Post>().Property(x => x.Title).HasMaxLength(256);
            modelBuilder.Entity<Post>().Property(x => x.Text).IsRequired();

            modelBuilder.Entity<Tag>().HasKey(x => x.Id);
            modelBuilder.Entity<Tag>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Tag>().Property(x => x.Name).HasMaxLength(256);

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Username).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Username).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(x => x.DisplayName).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.DisplayName).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(x => x.AuthCode).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.AuthCode).IsFixedLength().HasMaxLength(40);

            base.OnModelCreating(modelBuilder);
        }
    }
}
