using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}