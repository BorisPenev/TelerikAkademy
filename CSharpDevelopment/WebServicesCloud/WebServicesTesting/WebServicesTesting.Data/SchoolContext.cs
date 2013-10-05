using System.Data.Entity;
using WebServicesTesting.Model;

namespace WebServicesTesting.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("SchoolConnectionString")
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Mark> Marks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(x => x.SchoolId).IsOptional();

            base.OnModelCreating(modelBuilder);
        }
    }
}
