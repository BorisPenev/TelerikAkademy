using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Movies.Models;

namespace Movies.Controllers
{
    [Authorize]
    [ValidateInput(false)]
    public class MovieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Movie/
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: /Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", movie);
        }

        // GET: /Movie/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: /Movie/Create
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: /Movie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies
                .Include("LeadMaleActor")
                .Include("LeadFemaleActor")
                .FirstOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit", movie);
        }

        // POST: /Movie/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                // EF is not happy about the fact that I am trying to re-attach 
                // and update an object that is already placed in the cache
                // solution is:
                var entry = this.db.Entry(movie);

                if (entry.State == EntityState.Detached)
                {
                    var currentEntry = this.db.Movies.Find(movie.Id);
                    if (currentEntry != null)
                    {
                        var attachedEntry = this.db.Entry(currentEntry);
                        // remap the values from our model to the entry in DB
                        attachedEntry.CurrentValues.SetValues(movie);
                        attachedEntry.Entity.LeadMaleActor = movie.LeadMaleActor;
                        attachedEntry.Entity.LeadFemaleActor = movie.LeadFemaleActor;
                    }
                    else
                    {
                        this.db.Movies.Attach(movie);
                        entry.State = EntityState.Modified;
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: /Movie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", movie);
        }

        // POST: /Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
