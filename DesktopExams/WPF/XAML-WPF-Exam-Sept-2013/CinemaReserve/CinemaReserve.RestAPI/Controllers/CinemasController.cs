using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CinemaReserve.ResponseModels;
using CinemReserve.Data;
using CinemaReserve.RestAPI.Models;

namespace CinemaReserve.RestAPI.Controllers
{
    public class CinemasController : BaseApiController
    {
        public IQueryable<CinemaModel> GetAll()
        {
            return this.ExecuteOperationHandleExceptions(() =>
            {
                var context = new CinemaReserveDbContext();

                return context.Cinemas.Select(Parser.FromCinema);
            });
        }
                
        public IQueryable<MovieModel> GetMovies(int cinemaId)
        {
            return this.ExecuteOperationHandleExceptions(() =>
            {
                var context = new CinemaReserveDbContext();
                var theCinema = context.Cinemas.FirstOrDefault(c => c.Id == cinemaId);
                return theCinema.Movies.AsQueryable().Select(Parser.FromMovie);
            });
        }

        public IEnumerable<ProjectionModel> GetMovieProjections(int cinemaId, int movieId)
        {
            return this.ExecuteOperationHandleExceptions(() =>
            {
                var context = new CinemaReserveDbContext();
                var projections = context.Projections
                                         .Where(pr => pr.Cinema.Id == cinemaId &&
                                                      pr.Movie.Id == movieId &&
                                                      pr.Status.Status == "upcoming");
                return projections.Select(Parser.FromProjection).ToList();
            });
        }
    }
}