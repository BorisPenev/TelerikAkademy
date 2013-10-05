using System.Data.Entity;
using Bellini.Data.Library;

namespace Bellini.Data
{
    public class ProductTaxesDbContext : DbContext
    {
        public ProductTaxesDbContext() : base("TaxesSQLite")
        {

        }

        public DbSet<ProductTax> ProductTaxes { get; set; }
    }
}
