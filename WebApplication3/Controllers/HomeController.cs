using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Data.Entity;


namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private AuthorsContext db = new AuthorsContext();

        public ActionResult Index()
        {
            IEnumerable<Event> events = db.Events;
            // передаем все полученный объекты в динамическое свойство Books в ViewBag
            ViewBag.Events = events;

            return View(db.Authors.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //edit
        public ActionResult Edit(int? id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            ViewBag.Events = db.Events.ToList();
            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(Author author, int[] writtenEvents)
        {
            Author newAuthor = db.Authors.Find(author.Id);
            newAuthor.Name = author.Name;
            newAuthor.Surname = author.Surname;

            newAuthor.Events.Clear();
            if (writtenEvents != null)
            {
                //получаем написанные события
                foreach (var c in db.Events.Where(co => writtenEvents.Contains(co.Id)))
                {
                    newAuthor.Events.Add(c);
                }
            }

            db.Entry(newAuthor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}