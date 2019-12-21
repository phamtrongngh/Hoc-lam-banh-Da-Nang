using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    public class HoatdonghocvienController : CommonController
    {
        ModelCakes db = new ModelCakes();
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

            return View(db.News.Where(a=>a.NewsTypeId==3));
        }
        public ActionResult Details(string name)
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
            var list = db.News.Where(a => a.NewsTypeId == 3);
            var id = 0;
            foreach (var item in list)
            {
                if (HomeController.RemoveUnicode(item.NewsTitle).Equals(name))
                {
                    id = item.NewsId;
                }
            }
            return View(db.News.Find(id));
        }
    }

}