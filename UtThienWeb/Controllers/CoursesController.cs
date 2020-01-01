using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    delegate string dele(string name);

    public class CoursesController : CommonController
    {
        ModelCakes db = new ModelCakes();
        dele i = new dele(HomeController.RemoveUnicode);
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["cookieCHLB"];

            if (cookie != null)
            {
                Session["current_user"] = db.Accounts.Find(int.Parse(cookie["id"]));
            }

            if (Session["current_user"] == null)
            {
                Session["user"] = "<a href='' class='cart-btn' data-target='#login' data-toggle='modal'><i class='fa fa-user'></i><span>Đăng nhập</span></a>";
            }
            else
            {
                Session["user"] = "<a href='#'>Chào ";
                Session["user"] += ((UtThienWeb.Models.Account)Session["current_user"]).AccountName;
                Session["user"] += " </a>";
                Session["user"] += "<a href='/Accounts/Logout' class='cart-btn'><i class='fas fa-sign-out-alt'></i><span>Đăng xuất</span></a>";
            }
            return View(db.Courses);
        }
        public ActionResult GioHang()
        {
            HttpCookie cookie = Request.Cookies["cookieCHLB"];

            if (cookie != null)
            {
                Session["current_user"] = db.Accounts.Find(int.Parse(cookie["id"]));
            }

            if (Session["current_user"] == null)
            {
                Session["user"] = "<a href='' class='cart-btn' data-target='#login' data-toggle='modal'><i class='fa fa-user'></i><span>Đăng nhập</span></a>";
            }
            else
            {
                Session["user"] = "<a href='#'>Chào ";
                Session["user"] += ((UtThienWeb.Models.Account)Session["current_user"]).AccountName;
                Session["user"] += " </a>";
                Session["user"] += "<a href='/Accounts/Logout' class='cart-btn'><i class='fas fa-sign-out-alt'></i><span>Đăng xuất</span></a>";
            }
            return View();
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
            {
                var s = db.Courses.Find(id);
                s.CourseCountOrder = 1;
                ((List<Course>)Session["cart"]).Add(s);
            }
            return Json(((List<Course>)Session["cart"]).Count);
        }
        [HttpPost]
        public JsonResult ChangeQuantity(int quantity, int id)
        {
            foreach (var item in (List<Course>)Session["cart"])
            {
                if (item.CourseId == id)
                {
                    item.CourseCountOrder = quantity;
                    break;
                }
            }
            return Json("");
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
                var idOrder = db.Orders.Add(new Order { CreationDate = DateTime.Now, AccountId = idAccount }).OrderId;
                foreach (var item in (IEnumerable<Course>) Session["cart"])
                {
                    db.OrderDetails.Add(new OrderDetail{ CourseId = item.CourseId, OrderId = idOrder, OrderQuantity=item.CourseCountOrder });
                    db.Courses.Find(item.CourseId).CourseCountOrder++;
                }

                db.SaveChanges();
                Session.Remove("cart");
            }
            catch (Exception e)
            {

            }
            return Redirect("../");
        }

        public ActionResult Order()
        {
            try
            {
                var idOrder = db.Orders.Add(new Order { CreationDate = DateTime.Now, AccountId = ((Account)Session["current_user"]).AccountId }).OrderId;
                foreach (var item in (IEnumerable<Course>)Session["cart"])
                {
                    db.OrderDetails.Add(new OrderDetail { CourseId = item.CourseId, OrderId = idOrder, OrderQuantity=item.CourseCountOrder });
                    db.Courses.Find(item.CourseId).CourseCountOrder++;
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
            int re=1;
            foreach (var item in ((List<Course>)Session["cart"]))
            {
                if (item.CourseId == id)
                {
                    ((List<Course>)Session["cart"]).Remove(item);
                    re = (int)item.CoursePrice * item.CourseCountOrder;
                    break;
                }
            }
            return Json(re);
        }
    }
}