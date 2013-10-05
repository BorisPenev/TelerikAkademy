using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReserve.EntityModels
{
    public class Cinema
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }

        public Cinema()
        {
            this.Movies = new HashSet<Movie>();
            this.Projections = new HashSet<Projection>();
        }
    }
    
}