using System.Collections.Generic;

namespace CinemaReserve.EntityModels
{
    public class Reservation
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public string UserCode { get; set; }

        public ICollection<Seat> ReservedSeats { get; set; }
        public Reservation()
        {
            this.ReservedSeats = new HashSet<Seat>();
        }
    }
}