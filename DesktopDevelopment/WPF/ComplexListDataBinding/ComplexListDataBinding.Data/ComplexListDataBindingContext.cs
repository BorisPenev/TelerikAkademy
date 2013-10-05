using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexListDataBinding.Models;

namespace ComplexListDataBinding.Data
{
    public class ComplexListDataBindingContext : DbContext
    {
        public ComplexListDataBindingContext()
            : base("ComplexListDataBindingDB")
        {
            
        }

        public DbSet<Continents> Continentses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
