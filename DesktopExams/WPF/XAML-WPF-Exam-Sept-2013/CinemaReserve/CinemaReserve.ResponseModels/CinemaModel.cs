using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReserve.ResponseModels
{
    public partial class CinemaModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public partial class MovieModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public partial class MovieDetailsModel : MovieModel
    {
        public string Description { get; set; }

        public IEnumerable<string> Actors { get; set; }

        public IEnumerable<ProjectionModel> Projections { get; set; }
    }

    public partial class ProjectionModel
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public MovieModel Movie { get; set; }

        public CinemaModel Cinema { get; set; }
    }

    public partial class ProjectionDetailsModel : ProjectionModel
    {
        public IEnumerable<SeatModel> Seats { get; set; }
    }

    public partial class ReservationModel
    {
        public string UserCode { get; set; }
    }

    public partial class CreateReservationModel
    {
        public IEnumerable<SeatModel> Seats { get; set; }

        public string Email { get; set; }
    }

    public partial class SeatModel
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public string Status { get; set; }
    }

    public partial class RejectReservationModel
    {
        public string UserCode { get; set; }

        public string Email { get; set; }
    }
}