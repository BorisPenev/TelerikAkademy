using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        //
        // GET: /Category/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateCommand([DataSourceRequest] DataSourceRequest request, CategoryVewModel model)
        {
            var entity = new Category();
            if (ModelState.IsValid)
            {
                entity.Name = model.Name;
                Data.Categories.Add(entity);
                this.Data.SaveChanges();
                model.Id = entity.Id;
            }
            return Json(new[] { model }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadCommand([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Categories.All().OrderBy(x => x.Id).Select(CategoryVewModel.ToViewModel);
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        public JsonResult UpdateCommand([DataSourceRequest] DataSourceRequest request, CategoryVewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = this.Data.Categories.GetById(model.Id);
                entity.Name = model.Name;
                this.Data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        public JsonResult DestroyCommand([DataSourceRequest] DataSourceRequest request, CategoryVewModel model)
        {
            if (ModelState.IsValid)
            {
                this.Data.Categories.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCommand([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Categories.All().Select(CategoryVewModel.ToViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
	}
}