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
            return View(db.Courses.Where(a => a.CourseCatalogId == id));
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
        [HttpPost]
        public JsonResult PutToCart(int id)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Course>();
            }
            bool t = true;
            foreach (var item in (List<Course>)Session["cart"])
            {
                if (item.CourseId == id)
                {
                    t = false;
                }
            }
            if (t)
                ((List<Course>)Session["cart"]).Add(db.Courses.Find(id));
            return Json(((List<Course>)Session["cart"]).Count);
        }
        public ActionResult Cart()
        {

            return View();
        }
        [HttpPost]
        public ActionResult OrderAnonymoues(string name, string phone, string email)
        {
            try
            {
                var idAccount = db.Accounts.Add(new Account { AccountName = name, AccountPhone = phone, AccountEmail = email }).AccountId;
                var idOrder= db.Orders.Add(new Order { CreationDate = DateTime.Now, AccountId=idAccount }).OrderId;
                foreach (var item in (IEnumerable<Course>) Session["cart"])
                {
                    db.OrderDetails.Add(new OrderDetail { CourseId = item.CourseId, OrderId = idOrder });
                }

                db.SaveChanges();
                Session.Remove("cart");
            }
            catch (Exception e)
            {

            }
            return Redirect("../");
        }
        [HttpPost]
        public JsonResult RemoveCart(int id)
        {
            foreach (var item in ((List<Course>)Session["cart"]))
            {
                if (item.CourseId == id)
                {
                    ((List<Course>)Session["cart"]).Remove(item);
                    break;
                }
            }
            return Json("");
        }
    }
}