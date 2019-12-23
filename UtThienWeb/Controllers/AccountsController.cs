using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
using Facebook;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web.Configuration;
using System.Net.Configuration;

namespace UtThienWeb.Controllers
{

    public class AccountsController : Controller
    {
        ModelCakes db = new ModelCakes();
        // GET: Accounts
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



        public ActionResult Logout()
        {
            Session.Remove("current_user");
            if (Request.Cookies["cookieCHLB"] != null)
            {
                Response.Cookies["cookieCHLB"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult ConfirmEmail(string code)
        {
            var s = db.Accounts.SingleOrDefault(a => a.AccountRePassword.Equals(code));
            s.AccountConfirmEmail = true;
            s.AccountRePassword = null;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
                Random random = new Random();
                string num = random.Next(9000).ToString();
                account.AccountBOD = DateTime.Parse(dob).Date;
                account.TimeCreate = DateTime.Now;
                account.AccountRePassword = num;
                db.Accounts.Add(account);
                db.SaveChanges();
                var check = db.Accounts.SingleOrDefault(a => a.AccountUser.Equals(account.AccountUser));
                Session["current_user"] = check;
                HttpCookie cookie = new HttpCookie("cookieCHLB");
                cookie["username"] = check.AccountUser;
                cookie["id"] = check.AccountId.ToString();
                cookie["password"] = check.AccountPassword;
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("hoclambanhdanangpass@gmail.com", "doimatkhauroi1105"),
                    EnableSsl = true
                };
                var url = "https://localhost:44396/Accounts/ConfirmEmail?code=" + num;
                client.Send("hoclambanhdanangpass@gmail.com",email,"Kích Hoạt Email Học Làm Bánh Đà Nẵng", "Để kích hoạt email cho tài khoản "+user+", vui lòng nhấp vào link sau: "+url);
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
        [HttpPost]
        public ActionResult MailResetPassword(string username, string email)
        {
            var s = db.Accounts.SingleOrDefault(a => a.AccountUser.Equals(username));
            object mess;
            if (s != null)
            {
                if (s.AccountEmail.Equals(email) && s.AccountConfirmEmail == true)
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    finalString += "Aa";
                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential("hoclambanhdanangpass@gmail.com", "doimatkhauroi1105"),
                        EnableSsl = true
                    };
                    client.Send("hoclambanhdanangpass@gmail.com", "nghialovetran@gmail.com", "Khôi phục mật khẩu từ Học Làm Bánh Đà Nẵng", "Mã xác nhận của bạn là: " + finalString);
                    s.AccountRePassword = finalString;
                    db.SaveChanges();
                    ViewData["username"] = s.AccountUser;
                    mess = "Chúng tôi đã gửi mã xác nhận về email của bạn, hãy kiểm tra email và tiến hành đổi mật khẩu mới";
                }
                else
                {
                    mess = "Email không khớp với tên đăng nhập hoặc bạn chưa kích hoạt email cho tài khoản này";
                }
            }
            else
            {
                mess = "Tên đăng nhập không tồn tại";
            }
            
            return View(mess);
        }
        [HttpPost]
        public ActionResult ChangePass(string username,string code, string pass)
        {
            var account = db.Accounts.SingleOrDefault(a => a.AccountUser.Equals(username));
            object mess;
            if (account.AccountRePassword.Equals(code)){
                account.AccountPassword = pass;
                mess = "Đã đổi thành công mặt khẩu, vui lòng đăng nhập lại";
                db.SaveChanges();
            }
            else
            {
                mess = "Sai mã xác nhận";
            }
            return View(mess);
        }
    }
}