using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;

namespace UtThienWeb.Controllers
{
 
    [RoutePrefix("lien-he")]
    public class ContactController : CommonController
    {
        ModelCakes db = new ModelCakes();
        [Route]
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
            return View();
        }
        [HttpGet]
        public ActionResult Send()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Send(string name, string phone, string time, string content, string email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("hoclambanhdanangpass@gmail.com", "doimatkhauroi1105"),
                EnableSsl = true
            };
            client.Send("hoclambanhdanangpass@gmail.com", "hoclambanhdanang@gmail.com", "Yêu cầu liên hệ tư vấn ("+name+", "+phone+")", "Tên: "+name+Environment.NewLine+"SĐT: " +phone+Environment.NewLine +"Email: " +email+Environment.NewLine + "Thời gian tư vấn: " +time+Environment.NewLine + "Yêu cầu thêm: " +content);
            return View();
        }
    }
}