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
                ViewBag.user = "<a href='' class='cart-btn' data-target='#login' data-toggle='modal'><i class='fa fa-user'></i><span>Đăng nhập</span></a>";
            }
            else
            {
                ViewBag.user = "<a href='#'>Chào ";
                ViewBag.user += ((UtThienWeb.Models.Account)Session["current_user"]).AccountName;
                ViewBag.user += " </a>";
                ViewBag.user += "<a href='/Home/Logout' class='cart-btn'><i class='fas fa-sign-out-alt'></i><span>Đăng xuất</span></a>";
            }
            
            return View(db.Slides.ToArray());
        }
        [HttpPost]
        public ActionResult Login()
        {
            var user = Request.Form["userLogin"];
            var password = Request.Form["passwordLogin"];
            var checkbox = Request.Form["remember"];
            string mess = "";

            
            var check = db.Accounts.FirstOrDefault(a => a.AccountUser.Equals(user));
            if (check != null)
            {
                if (check.AccountPassword.Equals(password))
                {
                    Session["current_user"] = check;
                    if (checkbox == "1")
                    {
                        HttpCookie cookie = new HttpCookie("cookieCHLB");
                        cookie["username"] = check.AccountUser;
                        cookie["id"] = check.AccountId.ToString();
                        cookie["password"] = check.AccountPassword;
                        Response.Cookies.Add(cookie);
                    }
                }
                else
                {
                    mess = "Sai mật khẩu!";
                }
            }
            else
            {
                mess = "Tên đăng nhập không đúng!";
            }
            return Json(mess);
            
        }
        public ActionResult ShowAll()
        {
            return View(db.Accounts);
        }
        public ActionResult Logout()
        {
            Session.Remove("current_user");
            if (Request.Cookies["cookieCHLB"] != null)
            {
                Response.Cookies["cookieCHLB"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Register()
        {
            var user = Request.Form["userregister"];
            var email = Request.Form["emailregister"];
            var pass = Request.Form["passwordregister"];
            var fullname = Request.Form["fullnameregister"];
            var address = Request.Form["addressregister"];
            var phone = Request.Form["phoneregister"];
            var gender = (Request.Form["gender"] == "0") ? true : false;
            var dob = Request.Form["dob"];
            Account account = new Account();
            if (db.Accounts.SingleOrDefault(a => a.AccountUser.Equals(user)) != null)
            {
                return Json("existUsername");
            };
            if (db.Accounts.SingleOrDefault(a => a.AccountEmail.Equals(email)) != null)
            {
                return Json("existEmail");
            };
            try
            {
                account.AccountUser = user;
                account.AccountEmail = email;
                account.AccountPassword = pass;
                account.AccountPhone = phone;
                account.AccountName = fullname;
                account.AccountGender = gender;
                account.AccountRole = 0;
                account.AccountAddress = address;
                account.AccountBOD = DateTime.Parse(dob).Date;
                account.TimeCreate = DateTime.Now;
                db.Accounts.Add(account);
                db.SaveChanges();
                var check = db.Accounts.SingleOrDefault(a => a.AccountUser.Equals(account.AccountUser));
                Session["current_user"] = check;
                HttpCookie cookie = new HttpCookie("cookieCHLB");
                cookie["username"] = check.AccountUser;
                cookie["id"] = check.AccountId.ToString();
                cookie["password"] = check.AccountPassword;
                Response.Cookies.Add(cookie);
                return Json("Success");
            }
            catch (Exception e)
            {
                return Json(e);
            }

        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["client_id"],
                client_secret = ConfigurationManager.AppSettings["client_secret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["client_id"],
                client_secret = ConfigurationManager.AppSettings["client_secret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=name,email,gender,birthday,address,link");

                string fullname = me.name;
                string email = me.email;
                var gender = me.gender == "male" ? true : false;
                string bod = me.birthday;
                string address = me.address;
                string linkFacebook = me.link;
                var check = db.Accounts.SingleOrDefault(a => a.AccountEmail.Equals(email));
                if (check == null)
                {
                    Account account = new Account();
                    account.AccountName = fullname;
                    account.AccountEmail = email;
                    account.AccountGender = gender;
                    account.AccountAddress = address;
                    account.LinkFacebook = linkFacebook;
                    account.AccountRole = 0;
                    account.TimeCreate = DateTime.Now;
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    check = db.Accounts.SingleOrDefault(a => a.AccountEmail.Equals(email));
                }
                Session["current_user"] = check;
                HttpCookie cookie = new HttpCookie("cookieCHLB");
                cookie["username"] = check.AccountUser;
                cookie["id"] = check.AccountId.ToString();
                cookie["password"] = check.AccountPassword;
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}