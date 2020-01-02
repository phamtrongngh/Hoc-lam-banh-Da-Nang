using UtThienWeb.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UtThienWeb.Areas.Admin.Controllers
{
    public class CoursesController : CommonController
    {
        ModelCakes db = new ModelCakes();
        ModelCakes db2 = new ModelCakes();
        public ActionResult Index(int? id,string keyword,string trang)
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var courseList = new List<Course>();
            if (id == null)
            {
                courseList = db.Courses.OrderByDescending(a=>a.TimeCreate).ToList();
            }
            else
            {
                courseList = db.Courses.OrderByDescending(a => a.TimeCreate).Where(a => a.CourseCatalogId == id).ToList();
            }
            foreach (var item in courseList)
            {
                item.CourseCatalog = db2.CourseCatalogs.Find(item.CourseCatalogId);

            }
            ViewBag.catalog = db.CourseCatalogs;
            ViewBag.pagination = Math.Ceiling(((decimal)courseList.Count) / 9);
            if (trang == null)
            {
                trang = "1";
            }
            ViewBag.trang = trang;
            return View(courseList.Skip(trang == null ? 0 : (int.Parse(trang) - 1) * 9).Take(9));
            return View(courseList);
        }
        [HttpGet]
        public ActionResult Search(string keyword)
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var courseList = new List<Course>();
            if (keyword == null)
            {
                courseList = db.Courses.ToList();
            }
            else
            {
                courseList = (from s in db.Courses select s).AsEnumerable().Where(a => HomeController.RemoveUnicode(a.CourseName).Contains(HomeController.RemoveUnicode(keyword))).ToList();
                    
            }
            foreach (var item in courseList)
            {
                item.CourseCatalog = db2.CourseCatalogs.Find(item.CourseCatalogId);
            }
            ViewBag.catalog = db.CourseCatalogs;
            return View(courseList);
        }
        [HttpGet]
        public ActionResult CreateCourse()
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.catalog = db.CourseCatalogs;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateCourse(Course newCourse, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/img/"), fileName);
                file.SaveAs(path);
                newCourse.CourseThumbails = fileName;
            }
            else
            {
                newCourse.CourseThumbails = "no-image-available-icon-6.jpg";
            }
            if (ModelState.IsValid)
            {
                newCourse.TimeCreate = DateTime.Now;
                var catalog = Request.Form["catalog"];
                newCourse.CourseCatalogId = int.Parse(catalog);
                db.Courses.Add(newCourse);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCourse(int? CourseId)
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.catalog = db.CourseCatalogs;

            var newObj = db.Courses.Find(CourseId);
            newObj.CourseCatalog = db.CourseCatalogs.Find(newObj.CourseCatalogId);
            return View(newObj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCourse(int CourseId, Course course, string submit, HttpPostedFileBase file)
        {
            try
            {
                var model = db.Courses.SingleOrDefault(n => n.CourseId.Equals(CourseId));
                if (model != null)
                {
                    if (submit.Equals("Update"))
                    {
                        if (file != null)
                        {
                            string fileName = System.IO.Path.GetFileName(file.FileName);
                            string path = System.IO.Path.Combine(Server.MapPath("~/img/"), fileName);
                            file.SaveAs(path);
                            model.CourseThumbails = fileName;
                        }
                        else
                        {
                            model.CourseThumbails = "no-image-available-icon-6.jpg";
                        }
                        model.CoursePrice = course.CoursePrice;
                        model.CourseDescription = course.CourseDescription;
                        model.CourseTeacherName = course.CourseTeacherName;
                        model.CourseName = course.CourseName;
                        var catalog = Request.Form["catalog"];
                        model.CourseCatalogId = int.Parse(catalog);
                        db.SaveChanges();
                        ViewBag.Msg = "Update Success";
                    }
                    else
                    {
                        db.Courses.Remove(model);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Courses");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
               
            }
            return RedirectToAction("Index", "Courses");
        }
    }
}