namespace CinemaReserve.EntityModels
{
    public class Seat
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
          
        public virtual SeatStatus Status { get; set; }
    }
}