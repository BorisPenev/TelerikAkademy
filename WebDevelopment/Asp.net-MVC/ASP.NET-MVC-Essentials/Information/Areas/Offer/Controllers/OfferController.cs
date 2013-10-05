using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Information.Areas.Offer.Models;

namespace Information.Areas.Offer.Controllers
{
    public class OfferController : Controller
    {
        //
        // GET: /Offers/
        public ActionResult Index(OfferModel offerModel)
        {
            if (offerModel == null)
            {
                offerModel = new OfferModel();
            }

            offerModel.OfferTitles = new List<string>()
            {
                "offer 1",
                "offer 2",
                "offer 3",
                "offer 4",
                "offer 5"
            };

            return View(offerModel);
        }
	}
}