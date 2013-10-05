using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Information.Models;

namespace Information.Controllers
{
    public class TVController : Controller
    {
        //
        // GET: /TV/
        public ActionResult Index(TVmodel tvModel)
        {
            if (tvModel == null)
            {
                tvModel = new TVmodel();
            }

            tvModel.TvTitles = new List<string>()
            {
                "tv 1",
                "tv 2",
                "tv 3",
                "tv 4",
                "tv 5"
            };
            tvModel.Date = DateTime.Now;

            return View(tvModel);
        }
	}
}