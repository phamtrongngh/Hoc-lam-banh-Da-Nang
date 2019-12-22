using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    
    public class NewsController : Controller
    {
        ModelCakes db = new ModelCakes();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Views(int? id)
        {
            var s = db.News.Find(id);
            s.NewsViews += 1;
            db.SaveChanges();
            return Json("");
        }
    }
}