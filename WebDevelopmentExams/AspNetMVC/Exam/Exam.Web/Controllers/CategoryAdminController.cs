using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Exam.Web.Controllers
{
    public class CategoryAdminController : BaseController
    {
        [Authorize(Roles = "Admin")]
        //
        // GET: /CategoryAdmin/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadCommand([DataSourceRequest] DataSourceRequest request)
        {
            var model = this.Data.Categories.All().Select(CategoryViewModel.ToViewModel);
            return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCommand([DataSourceRequest] DataSourceRequest request, CategoryViewModel model)
        {
            var entity = this.Data.Categories.GetById(model.Id);

            entity.Name = model.Name;
            this.Data.SaveChanges();

            return Json(new[] { model }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyCommand([DataSourceRequest] DataSourceRequest request, CategoryViewModel model)
        {
            var tickets = Data.Tickets.All().Where(x => x.CategoryId == model.Id).ToList();
            foreach (var ticket in tickets)
            {
                Data.Tickets.Delete(ticket);
            }
            this.Data.Categories.Delete(model.Id);
            this.Data.SaveChanges();

            return Json(new[] { model }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

	}
}