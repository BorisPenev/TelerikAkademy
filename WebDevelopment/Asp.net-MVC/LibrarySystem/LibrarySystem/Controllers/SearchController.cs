using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SearchResult(string query)
        {
            var model = SearchQuery(query);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult SearchCommand([DataSourceRequest]DataSourceRequest request, string text)
        {
            var model = SearchQuery(text);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private IQueryable<SearchResultViewModel> SearchQuery(string text)
        {
            var result = Data.Books.All().Where(x => x.Title.ToLower().Contains(text.ToLower()) ||
                                                     x.Author.ToLower().Contains(text.ToLower()))
                .Select(SearchResultViewModel.ToViewModel);
            return result;
        }
	}
}