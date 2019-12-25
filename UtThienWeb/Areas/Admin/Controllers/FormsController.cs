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
            return View(db.Forms);
        }

    }
}
