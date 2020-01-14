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
        public ActionResult DashBoard()
        {
            return View();
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ"," ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y","-",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);

            }

            return text.ToLower();
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