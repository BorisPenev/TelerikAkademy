using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CinemaReserve.RestAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name:"DefaultCinemasApi",
                routeTemplate: "api/cinemas/{cinemaId}",
                defaults:new
                {
                    controller = "cinemas"
                });

            config.Routes.MapHttpRoute(
                name:"ProjectionsInCinemasApi",
                routeTemplate:"api/cinemas/{cinemaId}/projections/{movieId}",
                defaults:new
                {
                    controller = "cinemas"
                });

            config.Routes.MapHttpRoute(
                name: "DefaultMoviesApi",
                routeTemplate: "api/movies/{movieId}",
                defaults: new
                {
                    controller = "movies"
                });

            config.Routes.MapHttpRoute(
                name: "DefaultPorjectionsApi",
                routeTemplate: "api/projections/{projectionId}",
                defaults: new
                {
                    controller = "projections"
                });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}