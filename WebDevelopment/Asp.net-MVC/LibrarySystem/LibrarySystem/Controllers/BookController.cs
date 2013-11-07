using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookController : BaseController
    {
        //
        // GET: /Book/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateCommand([DataSourceRequest] DataSourceRequest request, BookViewModel model)
        {
            var entity = new Book();
            Data.Books.Add(entity);

            if (ModelState.IsValid)
            {
                CreateEditEntity(model, entity);

                model.Id = entity.Id;
                model.CategoryName = entity.Category.Name;
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadCommand([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Books.All().OrderBy(x => x.Id).Select(BookViewModel.ToViewModel);
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateCommand([DataSourceRequest] DataSourceRequest request, BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = this.Data.Books.GetById(model.Id);
                if (entity != null)
                {
                    CreateEditEntity(model, entity);
                }
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        private void CreateEditEntity(BookViewModel model, Book entity)
        {
            if (model.Category == null)
            {
                model.Category = Data.Categories.All().OrderBy(x => x.Id).Select(CategoryVewModel.ToViewModel).FirstOrDefault();
            }

            entity.Author = model.Author;
            if (model.Category != null)
            {
                model.CategoryName = model.Category.Name;
                var category = Data.Categories.GetById(model.Category.Id);
                if (category != null)
                {
                    entity.Category = category;
                }
                else
                {
                    entity.Category = new Category
                    {
                        Id = model.Category.Id,
                        Name = model.Category.Name
                    };
                }
            }
            entity.Description = model.Description;
            entity.Isbn = model.Isbn;
            entity.Title = model.Title;
            entity.WebSite = model.WebSite;

            this.Data.SaveChanges();
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
	}
}