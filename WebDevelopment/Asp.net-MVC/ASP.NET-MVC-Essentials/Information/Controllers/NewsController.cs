using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Information.Models;

namespace Information.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/
        public ActionResult Index(NewsModel newsModel)
        {
            if (newsModel == null)
            {
                newsModel = new NewsModel();
            }

            newsModel.NewsTitles = new List<string>()
            {
                "news1",
                "news2",
                "news3",
                "news4",
                "news5"
            };

            return View(newsModel);
        }

        public ActionResult Science(NewsModel newsModel)
        {
            if (newsModel == null)
            {
                newsModel = new NewsModel();
            }

            newsModel.NewsTitles = new List<string>()
            {
                "Science1",
                "Science2",
                "Science3",
                "Science4",
                "Science5"
            };

            return View(newsModel);
        }

        public ActionResult Culture(NewsModel newsModel)
        {
            if (newsModel == null)
            {
                newsModel = new NewsModel();
            }

            newsModel.NewsTitles = new List<string>()
            {
                "Culture1",
                "Culture2",
                "Culture3",
                "Culture4",
                "Culture5"
            };

            return View(newsModel);
        }
	}
}