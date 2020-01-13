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
        ModelCakes db2 = new ModelCakes();
        public ActionResult Index()
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
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
            db.CourseCatalogs.Add(new CourseCatalog { CourseCatalogName = name });
            db.SaveChanges();
            
            return Json("");
        }
        [HttpPost]
        public JsonResult DeleteMenu()
        {
            var id = int.Parse(Request.Form["id"]);
            var obj = db.Menus.Find(id);
            db.Menus.Remove(obj);
            var obj2 = db2.CourseCatalogs.FirstOrDefault(a => a.CourseCatalogName.Equals(obj.MenuLiName));
            db2.CourseCatalogs.Remove(obj2);
            db2.SaveChanges();
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