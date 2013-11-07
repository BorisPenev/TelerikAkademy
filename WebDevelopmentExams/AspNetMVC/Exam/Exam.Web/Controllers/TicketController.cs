using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Models;
using Exam.Web.Helpers;
using Exam.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace Exam.Web.Controllers
{
    
    public class TicketController : BaseController
    {
        [Authorize]
        //
        // GET: /Ticket/List
        public ActionResult List()
        {
            var model = Data.Tickets.All().Select(TicketsViewModel.ToViewModel);
            return View(model);
        }

        [Authorize]
        public JsonResult GetTickets([DataSourceRequest] DataSourceRequest request)
        {
            var model = Data.Tickets.All().Select(TicketsViewModel.ToViewModel);
            return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Create()
        {
            CreateTicketViewModel model = new CreateTicketViewModel();
            model.CategoryItems = Data.Categories.All().ToList().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            model.PriorityItems = Enum.GetValues(typeof(PriorityType)).Cast<PriorityType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var ticket = new Ticket();
                ticket.AuthorId = userId;
                ticket.CategoryId = model.SelectedCategory;
                ticket.Title = model.Title;
                ticket.Priority = model.SelectedPriority;
                ticket.ScreenshotURL = model.ScreenshotUrl;
                ticket.Description = model.Description;
             
                Data.Tickets.Add(ticket);

                var user = Data.ApplicationUser.All().FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    user.Points++;
                }
                
                Data.SaveChanges();
            }

            //model = new TicketViewModel();

            //model.CategoryItems = Data.Categories.All().ToList().Select(x => new SelectListItem()
            //{
            //    Value = x.Id.ToString(),
            //    Text = x.Name
            //}).ToList();

            //model.PriorityItems = Enum.GetValues(typeof(PriorityType)).Cast<PriorityType>().Select(v => new SelectListItem
            //{
            //    Text = v.ToString(),
            //    Value = ((int)v).ToString()
            //}).ToList();

            return RedirectToAction("Create");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                RedirectToAction("Index", "Home");
            }

            var model = this.Data.Tickets.All()
                            .Where(x => x.Id == id.Value)
                            .Select(TicketDetailsVewModel.ToViewModel)
                            .FirstOrDefault();

            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();

                this.Data.Comments.Add(new Comment()
                {
                    AuthorId = userId,
                    Content = commentModel.Comment,
                    TicketId = commentModel.TicketId,
                });

                this.Data.SaveChanges();

                var viewModel = new CommentViewModel { AuthorName = username, Content = commentModel.Comment };
                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        [Authorize]
        public JsonResult GetAllCategories()
        {
            var model = Data.Categories.All().Select(x=> new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Search(string categorySearch)
        {
            var result = this.Data.Tickets.All();

            if (!string.IsNullOrEmpty(categorySearch))
            {
                var categoryId = categorySearch.ToInt();
                result = result.Where(x => x.Category.Id.Equals(categoryId));
            }

            var endResult = result.Select(TicketsViewModel.ToViewModel);

            return View(endResult);
        }
	}
}