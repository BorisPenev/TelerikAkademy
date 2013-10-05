namespace CinemReserve.Data.Migrations
{
    using CinemaReserve.EntityModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CinemaReserveDbContext>
    {
        private static Random rand = new Random();

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        private SeatStatus FreeSeatStatus;
        private ProjectionStatus UpcomingReservationStatus;

        protected override void Seed(CinemaReserveDbContext context)
        {
            FreeSeatStatus = context.SeatStatus.FirstOrDefault(st => st.Status == "free") ?? new SeatStatus()
            {
                Status = "free"
            };

            UpcomingReservationStatus = context.ProjectionStatuses.FirstOrDefault(st => st.Status == "upcoming") ?? new ProjectionStatus()
            {
                Status = "upcoming"
            };

            for (int i = 0; i < 5; i++)
            {
                Cinema newCinema = this.GetCinema(i);
                context.Cinemas.Add(newCinema);
            }
            context.SaveChanges();
        }

        private Cinema GetCinema(int cinemaIndex)
        {
            var movies = new List<Movie>();
            for (int i = 0; i < 10; i++)
            {
                movies.Add(this.GetMovie(cinemaIndex, i));
            }

            string name = "Cinema #" + cinemaIndex;
            var projections = movies.SelectMany(m => m.Projections).ToList();
            var cinema = new Cinema()
            {
                Name = name,
                Movies = movies,
                Projections = projections
            };

            return cinema;
        }

        private Movie GetMovie(int cinemaIndex, int movieIndex)
        {
            var title = string.Format("Movie #{0} in cinema {1}", movieIndex, cinemaIndex);
            var description = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                description += title;
            }
            var actors = new Actor[]
            {
                new Actor() { Fullname = "First Actor" },
                new Actor() { Fullname = "First Actor" },
                new Actor() { Fullname = "First Actor" },
            };

            var projections = new List<Projection>();
            for (int i = 0; i < 5; i++)
            {
                projections.Add(this.GetProjection());
            }

            var movie = new Movie()
            {
                Title = title,
                Description = description,
                Duration = rand.Next(90, 180),
                Year = rand.Next(1960, 2013),
                Actors = actors,
                Projections = projections
            };
            return movie;
        }

        private Projection GetProjection()
        {
            var time = DateTime.Now;

            List<Seat> seats = new List<Seat>();
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 18; col++)
                {
                    seats.Add(new Seat()
                    {
                        Row = row,
                        Column = col,
                        Status = FreeSeatStatus
                    });
                }
            }

            var projection = new Projection()
            {
                ProjectionTime = time,
                Seats = seats,
                Status = UpcomingReservationStatus
            };

            return projection;
        }
    }
}