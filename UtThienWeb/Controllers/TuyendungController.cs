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
            return View(db.News.Where(a=>a.NewsTypeId==2));
        }
    }
}