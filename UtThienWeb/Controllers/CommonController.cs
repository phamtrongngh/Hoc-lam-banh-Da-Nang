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

        public ActionResult ShowDetails(string name, string trang, string order)
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
            Course course = (from s in db.Courses select s).AsEnumerable().FirstOrDefault(a => HomeController.RemoveUnicode(a.CourseName) == name);
            if (course != null)
            {
                ViewBag.topViews = db.News.OrderByDescending(a => a.NewsViews).Take(6);
                ViewBag.catalog = db.CourseCatalogs.SingleOrDefault(a => a.CourseCatalogId == course.CourseCatalogId).CourseCatalogName;

                return View("ShowDetailsCourses", course);
            }
            News news = (from s in db.News select s).AsEnumerable().FirstOrDefault(a => HomeController.RemoveUnicode(a.NewsTitle) == name);
            if (news != null)
            {

                ViewBag.topViews = db.News.OrderByDescending(a => a.NewsViews).Take(6);
                ViewBag.catalog = db.NewsCatalogs.Find(news.NewsTypeId).NewsCatalogName;
                return View("ShowDetailsNews", news);
            }
            else
            {
                CourseCatalog catalog = (from s in db.CourseCatalogs select s).AsEnumerable().SingleOrDefault(a => HomeController.RemoveUnicode(a.CourseCatalogName) == name);
                ViewBag.topViews = db.News.OrderByDescending(a => a.NewsViews).Take(6);
                var courses = db.Courses.Where(a => a.CourseCatalogId.Equals(catalog.CourseCatalogId)).AsEnumerable().ToList();
                if (order == null)
                {

                }
                else 
                if (order.Equals("new")){
                    courses=courses.OrderByDescending(a => a.TimeCreate).ToList();
                }
                else if(order.Equals("price"))
                {
                    courses=courses.OrderByDescending(a => a.CoursePrice).ToList();
                }
                else if(order.Equals("hot"))
                {
                    courses = courses.OrderByDescending(a => a.CourseCountOrder).ToList();
                }
                ViewBag.catalog = catalog;
                ViewBag.pagination = Math.Ceiling((decimal)courses.Count / 9);
                if (trang == null)
                {
                    trang = "1";
                }
                ViewBag.trang = trang;
                ViewBag.order = order;
                return View("ShowListCourses", courses.Skip(trang == null ? 0 : (int.Parse(trang) - 1) * 9).Take(9));
            }
        }
    }
}