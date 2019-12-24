using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    
    public class NewsController : Controller
    {
        ModelCakes db = new ModelCakes();
        [ValidateInput(false)]
        public ActionResult Details(News news)
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
            //var list = db.News;
            //News id = new News();
            //foreach (var item in list)
            //{
            //    if (HomeController.RemoveUnicode(item.NewsTitle).Equals(name))
            //    {
            //        id = item;
            //    }
            //}
            ViewBag.topViews = db.News.OrderByDescending(a => a.NewsViews).Take(6);
            ViewBag.catalog=db.NewsCatalogs.Find(news.NewsTypeId).NewsCatalogName;
            return View(news);
        }
        [HttpPost]
        public JsonResult Views(int? id)
        {
            var s = db.News.Find(id);
            s.NewsViews += 1;
            db.SaveChanges();
            return Json("");
        }
    }
}