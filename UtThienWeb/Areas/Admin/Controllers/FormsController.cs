using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Areas.Admin.Models;
namespace UtThienWeb.Areas.Admin.Controllers
{
    public class FormsController : Controller
    {
        ModelCakes db = new ModelCakes();
        public ActionResult Index()
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(db.Forms.OrderByDescending(a=>a.FormCreationDate));
        }
        public ActionResult Apply(int id, string apply)
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (apply.Equals("success"))
            {
                db.Forms.Find(id).FormStatus = "succcess";
            }
            if (apply.Equals("failed"))
            {
                db.Forms.Find(id).FormStatus = "failed";
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
