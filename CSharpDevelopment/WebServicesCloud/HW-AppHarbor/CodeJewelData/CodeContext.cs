using CodeJewelModels;
using System;
using System.Data.Entity;
using System.Linq;

namespace CodeJewelData
{
    public class CodeContext : DbContext
    {
        public CodeContext() :base("CodeDb")
        {
        }

        public DbSet<Vote> Votes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CodeJewel> CodeJewels { get; set; }

    }
}
