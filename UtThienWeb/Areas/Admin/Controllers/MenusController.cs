using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Areas.Admin.Models;

namespace UtThienWeb.Areas.Admin.Controllers
{
    public class MenusController : Controller
    {
        ModelCakes db = new ModelCakes();
        public ActionResult Index()
        {

            var menu = db.Menus;
            ViewBag.slide = db.Slides;
            foreach (var item in menu)
            {
                if (item.MenuSubLi != null)
                {
                    db.Menus.Find(item.MenuSubLi).Menu1.Add(item);
                }
            }
            return View(menu);
        }
        [HttpPost]
        public JsonResult AddMenu()
        {

            var id = int.Parse(Request.Form["id"]);
            var name = Request.Form["name"];
            Menu menu = new Menu();
            menu.MenuSubLi = id;
            menu.MenuLiName = name;
            db.Menus.Add(menu);
            db.SaveChanges();
            
            return Json("");
        }
        [HttpPost]
        public JsonResult DeleteMenu()
        {

            var id = int.Parse(Request.Form["id"]);

            db.Menus.Remove(db.Menus.Find(id));
            db.SaveChanges();

            return Json("");
        }
        [HttpPost]
        public ActionResult ChangeSlide(HttpPostedFileBase file, string slideNumber)
        {

            if (file != null)
            {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/img/"), fileName);
                file.SaveAs(path);
                db.Slides.Find(int.Parse(slideNumber)).SlideImages = fileName;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}