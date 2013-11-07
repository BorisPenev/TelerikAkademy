using System;
using System.Linq;
using System.Web.Mvc;
using Movies.Models;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}