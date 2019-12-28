using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
using Facebook;
using System.Configuration;

namespace UtThienWeb.Controllers
{
    [RoutePrefix("")]
    public class HomeController : CommonController
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
                Session["user"] = "<a href='' class='cart-btn' data-target='#login' data-toggle='modal'><i class='fa fa-user'></i><span class='login-text'>Đăng nhập</span></a>";
            }
            else
            {
                Session["user"] = "<a href='#'>Chào ";
                Session["user"] += ((UtThienWeb.Models.Account)Session["current_user"]).AccountName;
                Session["user"] += " </a>";
                Session["user"] += "<a href='/Accounts/Logout' class='cart-btn'><i class='fas fa-sign-out-alt'></i><span>Đăng xuất</span></a>";
            }

            ViewBag.course = db.Courses.Take(6);
            return View(db.Slides.ToArray());
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ"," ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y","-",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
             
            }
            
            return text.ToLower();
        }
        
        [HttpGet]
        public ActionResult Search(string keyword)
        {
            keyword = HomeController.RemoveUnicode(keyword);
            List<Search> s=(from n in db.News select n).AsEnumerable().Where(a => HomeController.RemoveUnicode(a.NewsTitle).Contains(keyword)).Select(a=>new Search {SearchName=a.NewsTitle,SearchImage=a.NewsThumbails }).ToList<Search>();
            s.AddRange((from n in db.Courses select n).AsEnumerable().Where(a => HomeController.RemoveUnicode(a.CourseName).Contains(keyword)).Select(a => new Search { SearchName = a.CourseName, SearchImage = a.CourseThumbails }).ToList<Search>());
            return View(s);
        }

    }
}