using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaReserve.EntityModels;

namespace CinemReserve.Data
{
    public class CinemaReserveDbContext : DbContext
    {
        public IDbSet<Cinema> Cinemas { get; set; }
        public IDbSet<Movie> Movies { get; set; }
        public IDbSet<Projection> Projections { get; set; }
        public IDbSet<Reservation> Reservations { get; set; }
        public IDbSet<ProjectionStatus> ProjectionStatuses { get; set; }

        public IDbSet<Seat> Seats { get; set; }

        public IDbSet<SeatStatus> SeatStatus { get; set; }

        public IDbSet<Actor> Actors { get; set; }

        public CinemaReserveDbContext()
            : base("CinemaReserveDb")
        {
        }
    }
}
