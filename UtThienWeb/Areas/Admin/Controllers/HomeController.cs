using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UtThienWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        
        
        [HttpPost]
        public JsonResult UploadImages()
        {
            HttpPostedFileBase[] File1 = Request.Files.GetMultiple("filess").ToArray();
            List<string> list = new List<string>();
            foreach (HttpPostedFileBase item in File1)
            {


                string path = Server.MapPath("~/img/");

                string extensionName = System.IO.Path.GetExtension(item.FileName);

                string finalFileName = DateTime.Now.Ticks.ToString() + extensionName;

                item.SaveAs(path + finalFileName);

                list.Add(finalFileName);
            }
            return Json(list);
        }
    }
}