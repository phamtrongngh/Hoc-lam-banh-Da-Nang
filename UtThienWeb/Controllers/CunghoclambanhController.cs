using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    public class CunghoclambanhController : CommonController
    {
        ModelCakes db = new ModelCakes();
        public ActionResult Index(string trang)
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
            ViewBag.topViews = db.News.OrderByDescending(a=>a.NewsViews).Take(6);
            var news = db.News.Where(a => a.NewsTypeId == 1).OrderByDescending(a=>a.NewsDate).ToList();
            ViewBag.pagination = Math.Ceiling(((decimal)news.Count) / 9);
            if (trang == null)
            {
                trang = "1";
            }
            ViewBag.trang = trang;
            return View(news.Skip(trang == null ? 0 : (int.Parse(trang) - 1) * 9).Take(9));
        }
        
    }
}