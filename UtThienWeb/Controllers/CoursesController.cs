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
        public ActionResult khoabanhchuyensau()
        {
            return View();
        }
        public ActionResult khoabanhngoaikhoa()
        {
            return View();
        }
        public ActionResult baivietkhoahoc()
        {
            return View();
        }
    }
}