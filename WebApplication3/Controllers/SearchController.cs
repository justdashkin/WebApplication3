using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        private AuthorsContext db = new AuthorsContext();

        [HttpPost]
        public ActionResult Index(FormCollection request)
        {
            string word = request["search"];
            ViewBag.Events1 = db.Events.Where(e => e.Name.Contains(word) || e.Description.Contains(word)).ToList();
            ViewBag.Word = word;
            return View();

        }
    }
}