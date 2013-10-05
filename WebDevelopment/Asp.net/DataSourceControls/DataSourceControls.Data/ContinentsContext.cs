using System.Data.Entity;
using DataSourceControls.Models;

namespace DataSourceControls.Data
{
    class ContinentsContext : DbContext
    {
        public ContinentsContext()
            : base("ContinentsConnectionString")
        {
            
        }

        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
