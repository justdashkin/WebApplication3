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

            

            var cookie = new HttpCookie("LastVisit")
            {
                Name = "test_cookie",
                Value = DateTime.Now.ToString("dd.MM.yyyy"),
                Expires = DateTime.Now.AddMinutes(10),
            };
            Response.SetCookie(cookie);
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

        //редактирование 
        [HttpGet]
        public ActionResult EditEvent(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Event event1 = db.Events.Find(id);

            if (event1 == null)
            {
                return HttpNotFound();
            }
            return View(event1);
        }

        //сохранение редактирования
        [HttpPost]
        public ActionResult EditEvent(Event event1)
        {
            db.Entry(event1).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //добавление собітия
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Event event1)
        {
            db.Events.Add(event1);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //удаление события
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Event b = db.Events.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Event b = db.Events.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Events.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //детали по событию
        public ActionResult EventDetails(int id = 0)
        {
            Event event1 = db.Events.Find(id);
            if (event1 == null)
            {
                return HttpNotFound();
            }
            return View(event1);
        }

        //редактирование со связью многие ко многим для события
        public ActionResult EditEventAuthors(int? id)
        {
            Event event1 = db.Events.Find(id);
            if (event1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.Authors = db.Authors.ToList();
            return View(event1);
        }

        [HttpPost]
        public ActionResult EditEventAuthors(Event event1, int[] writtenAuthors)
        {
            Event newEvent = db.Events.Find(event1.Id);
            newEvent.Name = event1.Name;
            newEvent.Description = event1.Description;

            newEvent.Authors.Clear();
            if (writtenAuthors != null)
            {
                //получаем авторов
                foreach (var c in db.Authors.Where(co => writtenAuthors.Contains(co.Id)))
                {
                    newEvent.Authors.Add(c);
                }
            }

            db.Entry(newEvent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      

    } 
}
