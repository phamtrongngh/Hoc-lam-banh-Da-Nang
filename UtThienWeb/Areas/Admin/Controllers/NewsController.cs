using UtThienWeb.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UtThienWeb.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        ModelCakes db = new ModelCakes();
        ModelCakes db2 = new ModelCakes();
        public ActionResult Index()
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var newList = db.News;
            foreach (var item in newList)
            {
                item.NewsCatalog = db2.NewsCatalogs.Find(item.NewsTypeId);
 
            }
            return View(newList);
        }
        [HttpGet]
        public ActionResult CreateNews()
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.catalog = db.NewsCatalogs;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateNews(News news, HttpPostedFileBase file)
        {
            
            if (file != null)
            {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/img/"), fileName);
                file.SaveAs(path);
                news.NewsThumbails = fileName;
            }
            else
            {
                news.NewsThumbails = "no-image-available-icon-6.jpg";
            }
            if (ModelState.IsValid)
            {
                news.NewsDate = DateTime.Now;
                var catalog = Request.Form["catalog"];
                news.NewsTypeId = int.Parse(catalog);
                db.News.Add(news);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditNews(int? NewsId)
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.catalog = db.NewsCatalogs;

            var newObj = db.News.Find(NewsId);
            newObj.NewsCatalog = db.NewsCatalogs.Find(newObj.NewsTypeId);
            return View(newObj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditNews(int NewsId, News news, string submit, HttpPostedFileBase file)
        {
            try
            {
                var model = db.News.SingleOrDefault(n => n.NewsId.Equals(NewsId));
                if (model != null)
                {
                    if (submit.Equals("Update"))
                    {
                        if (file != null)
                        {
                            string fileName = System.IO.Path.GetFileName(file.FileName);
                            string path = System.IO.Path.Combine(Server.MapPath("~/img/"), fileName);
                            file.SaveAs(path);
                            model.NewsThumbails = fileName;
                        }
                        else
                        {
                            model.NewsThumbails = "no-image-available-icon-6.jpg";
                        }
                        model.NewsAuthor = news.NewsAuthor;
                        model.NewsCatalog = news.NewsCatalog;
                        model.NewsContent = news.NewsContent;
                        model.NewsTitle = news.NewsTitle;
                        var catalog = Request.Form["catalog"];
                        model.NewsTypeId = int.Parse(catalog);
                        db.SaveChanges();
                        ViewBag.Msg = "Update Success";
                    }
                    else
                    {
                        db.News.Remove(model);
                        db.SaveChanges();
                        return RedirectToAction("Index", "News");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
                throw;
            }
            return RedirectToAction("Index", "News");
        }
    }
}