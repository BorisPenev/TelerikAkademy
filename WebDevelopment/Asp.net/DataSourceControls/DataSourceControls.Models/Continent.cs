using System.Collections.Generic;

namespace DataSourceControls.Models
{
    public class Continent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Country> Countries { get; set; }

        public Continent()
        {
            this.Countries = new HashSet<Country>();
        }
    }
}
