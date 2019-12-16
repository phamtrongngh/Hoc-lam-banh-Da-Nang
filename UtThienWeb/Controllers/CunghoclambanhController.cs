using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    public class CunghoclambanhController : CommonController
    {
        ModelCakes db = new ModelCakes();
        public ActionResult Index()
        {
            return View(db.News.Where(a=>a.NewsTypeId==1));
        }
        public ActionResult Details(string name)
        {
            
            var list = db.News.Where(a=>a.NewsTypeId==1);
            var id = 0;
            foreach (var item in list)
            {
                if (HomeController.RemoveUnicode(item.NewsTitle).Equals(name))
                {
                    id = item.NewsId;
                }
            }
            return View(db.News.Find(id));
        }
    }
}