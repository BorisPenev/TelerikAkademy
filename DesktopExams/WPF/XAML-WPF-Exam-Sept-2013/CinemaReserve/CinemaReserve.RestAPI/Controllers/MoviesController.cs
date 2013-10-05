using CinemaReserve.ResponseModels;
using CinemaReserve.RestAPI.Models;
using CinemReserve.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemaReserve.RestAPI.Controllers
{
    public class MoviesController : BaseApiController
    {
        public IQueryable<MovieModel> GetAll()
        {
            return this.ExecuteOperationHandleExceptions(() =>
            {
                var context = new CinemaReserveDbContext();
                return context.Movies.Select(Parser.FromMovie);
            });
        }

        public MovieDetailsModel GetMovieInfo(int movieId)
        {
            return this.ExecuteOperationHandleExceptions(() =>
            {
                var context = new CinemaReserveDbContext();
                var theMovie = context.Movies.FirstOrDefault(m => m.Id == movieId);
                if (theMovie == null)
                {
                    throw new ArgumentNullException("No such movie");
                }

                return Parser.ToMovieDetails(theMovie);
            });
        }

        public IQueryable<MovieModel> GetByKeyword(string keyword)
        {
            return this.ExecuteOperationHandleExceptions(() =>
            {
                var keywordToLower = keyword.ToLower();
                var context = new CinemaReserveDbContext();
                return context.Movies
                              .Where(m => m.Title.ToLower().Contains(keywordToLower) ||
                                          m.Description.ToLower().Contains(keywordToLower))
                              .Select(Parser.FromMovie);
            });                       
        }
    }
}