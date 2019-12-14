using Admin.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace UtThienWeb.Areas.Admin.Controllers
{
    
    public class AccountController : Controller
    {
        ModelCakes db = new ModelCakes();
        
        public ActionResult Index()
        {
            return View(db.Accounts);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            try
            {
                var account = db.Accounts.SingleOrDefault(a => a.AccountUser.Equals(username));
                if (account != null)
                {

                    if (account.AccountPassword.Equals(password))
                    {
                        Session["uName"] = username;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Msg = "Sai Mật Khẩu";
                    }
                }
                else
                {
                    ViewBag.Msg = "Sai Tên Tài Khoản";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
                throw;
            }
            return View();
        }
        public ActionResult AccountDetails(int? AccountId)
        {
            return View(db.Accounts.Find(AccountId));
        }
    }
}