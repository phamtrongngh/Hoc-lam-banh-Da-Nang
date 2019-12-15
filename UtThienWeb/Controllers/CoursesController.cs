using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    public class CoursesController : CommonController
    {
        ModelCakes db = new ModelCakes();

        public ActionResult Index()
        {
            return View(db.Courses);
        }
        public ActionResult GioHang()
        {
            return View();
        }
        public ActionResult CoursesItem(string name)
        {
            var list = db.CourseCatalogs;
            var id = 0;
            foreach (var item in list)
            {
                if (HomeController.RemoveUnicode(item.CourseCatalogName).Equals(name))
                {
                    id = item.CourseCatalogId;
                }
            }
            return View(db.Courses.Where(a=>a.CourseCatalogId==id));
        }
        
        public ActionResult CourseDetails(string name)
        {
          
            var list = db.Courses;
            var id = 0;
            foreach (var item in list)
            {
                if (HomeController.RemoveUnicode(item.CourseName).Equals(name))
                {
                    id = item.CourseId;
                }
            }
            return View(db.Courses.Find(id));
        }
        public ActionResult baivietkhoahoc()
        {
            return View();
        }
    }
}