using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellini.Data
{
    public class Supermarket
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Sale> Sales { get; set; } 
    }
}
