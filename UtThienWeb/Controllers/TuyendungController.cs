using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    public class TuyendungController : CommonController
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
            return View(db.News.Where(a=>a.NewsTypeId==2));
        }
        [HttpPost]
        public ActionResult SendForm(string email, string name, string phone, string exp, int submit, string title)
        {
            try
            {
                db.Forms.Add(new Form { FormEmail= email, FormName=name, FormPhone= phone, FormExperience = exp, NewsId=submit, NewsTitle=title, FormCreationDate=DateTime.Now, FormStatus="Đang chờ liên lạc"});
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
            return Redirect("/tuyen-dung");
        }
    }
}