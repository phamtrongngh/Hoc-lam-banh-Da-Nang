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
            var ordersList = db.Orders;
            foreach (var item in ordersList)
            {
                item.Account = db.Accounts.Find(item.AccountId);
                item.OrderDetails = db.OrderDetails.Where(a=>a.OrderId==item.OrderId).ToList();
            }           
            return View(ordersList);
        }
    }
}