using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{

    public class CommonController : Controller
    {
        ModelCakes db = new ModelCakes();
        public ActionResult CoursesNewsHandler(string name)
        {
            Course course = (from s in db.Courses select s).AsEnumerable().SingleOrDefault(a => HomeController.RemoveUnicode(a.CourseName) == name);
            if (course != null)
            {

            }
            return View();
        }
    }
}