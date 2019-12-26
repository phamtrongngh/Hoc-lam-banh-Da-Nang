using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Areas.Admin.Models;
namespace UtThienWeb.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        ModelCakes db = new ModelCakes();
        public ActionResult Index()
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var ordersList = db.Orders;
            foreach (var item in ordersList)
            {
                item.Account = db.Accounts.Find(item.AccountId);
                item.OrderDetails = db.OrderDetails.Where(a=>a.OrderId==item.OrderId).ToList();
            }           
            return View(ordersList);
        }
        public ActionResult Apply(int id, string apply)
        {
            if (apply.Equals("success"))
            {
                db.Orders.Find(id).OrderStatus = true;
            }
            if (apply.Equals("failed"))
            {
                db.Orders.Find(id).OrderStatus = false;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}