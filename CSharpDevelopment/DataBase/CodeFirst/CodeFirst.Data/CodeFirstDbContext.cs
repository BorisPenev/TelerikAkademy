using System.Data.Entity;
using CodeFirst.Model;

namespace CodeFirst.Data
{
    public class CodeFirstDbContext : DbContext
    {
        public CodeFirstDbContext()
            : base("CodeFirstDb")
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasKey(x => x.Id);
            modelBuilder.Entity<Course>().Property(x => x.Name).IsUnicode(true);
            modelBuilder.Entity<Course>().Property(x => x.Name).HasMaxLength(255);
            modelBuilder.Entity<Course>().Property(x => x.Description).HasMaxLength(255);

            modelBuilder.Entity<Homework>().Property(x => x.Content).IsUnicode(true);
            modelBuilder.Entity<Homework>().Property(x => x.Content).HasMaxLength(255);

            modelBuilder.Entity<Student>().Property(x => x.Name).IsUnicode(true);
            modelBuilder.Entity<Student>().Property(x => x.Name).HasMaxLength(255);
            //modelBuilder.Configurations.Add(new TagMappings());

            base.OnModelCreating(modelBuilder);
        }
    }
}
