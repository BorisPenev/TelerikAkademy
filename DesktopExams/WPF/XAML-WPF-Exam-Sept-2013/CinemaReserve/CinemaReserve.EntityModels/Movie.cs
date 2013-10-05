using System.Collections.Generic;

namespace CinemaReserve.EntityModels
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; }

        public int Year { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public Movie()
        {
            this.Cinemas = new HashSet<Cinema>();
            this.Projections = new HashSet<Projection>();
            this.Actors = new HashSet<Actor>();
        }
    }
}