using System.Data.Entity;
using Bellini.Data.Library;
using Bellini.Library;

namespace Bellini.Data
{
    public class SupermarketSQLDbContext : DbContext
    {
        public SupermarketSQLDbContext()
            : base("SupermarketSQLdb")
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Measure> Measures { get; set; }

        public DbSet<Supermarket> Supermarkets { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<VendorExpenseSql> VendorExpenses { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Expense>().HasKey(x => x.Id);
            //modelBuilder.Entity<Measure>().HasKey(x => x.ID);
            //modelBuilder.Entity<Measure>().Property(x => x.ID).IsOptional();


            //modelBuilder.Entity<Course>().Property(x => x.Name).IsUnicode(true);
            //modelBuilder.Entity<Course>().Property(x => x.Name).HasMaxLength(255);
            //modelBuilder.Entity<Course>().Property(x => x.Description).HasMaxLength(255);

            //modelBuilder.Entity<Homework>().Property(x => x.Content).IsUnicode(true);
            //modelBuilder.Entity<Homework>().Property(x => x.Content).HasMaxLength(255);

            //modelBuilder.Entity<Student>().Property(x => x.Name).IsUnicode(true);
            //modelBuilder.Entity<Student>().Property(x => x.Name).HasMaxLength(255);
            //modelBuilder.Configurations.Add(new TagMappings());

            base.OnModelCreating(modelBuilder);
        }
    }
}
