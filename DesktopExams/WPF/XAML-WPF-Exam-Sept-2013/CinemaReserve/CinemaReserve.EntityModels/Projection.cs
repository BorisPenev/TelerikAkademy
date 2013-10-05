using System;
using System.Collections.Generic;

namespace CinemaReserve.EntityModels
{
    public class Projection
    {
        public int Id { get; set; }

        public DateTime ProjectionTime { get; set; }

        public virtual Cinema Cinema { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual ProjectionStatus Status { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public Projection()
        {
            this.Seats = new HashSet<Seat>();
            this.Reservations = new HashSet<Reservation>();
        }

    }
}