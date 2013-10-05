using System.Collections.Generic;

namespace DataSourceControls.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public byte[] Flag { get; set; }
        public int ContinentId { get; set; }
        public virtual Continent Continent { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public Country()
        {
            this.Cities = new HashSet<City>();
        }
    }
}
