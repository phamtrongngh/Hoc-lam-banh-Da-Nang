using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    [RoutePrefix("khoa-hoc")]
    public class CoursesController : CommonController
    {
        ModelCakes db = new ModelCakes();
        [Route]
        public ActionResult Index()
        {
            return View(db.Courses);
        }
        public ActionResult GioHang()
        {
            return View();
        }
        [Route("khoa-banh-chuyen-sau")]
        public ActionResult khoabanhchuyensau()
        {
            return View();
        }
        [Route("khoa-banh-ngoai-khoa")]
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