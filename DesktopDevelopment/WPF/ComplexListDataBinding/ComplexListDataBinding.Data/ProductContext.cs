using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexListDataBinding.Models;

namespace ComplexListDataBinding.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext()
            : base("ProductConnectionString")
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
