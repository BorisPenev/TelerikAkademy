using System.Collections.Generic;

namespace ComplexListDataBinding.Models
{
    public class Continents
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Country> Countries { get; set; }

        public Continents()
        {
            this.Countries = new HashSet<Country>();
        }
    }
}
