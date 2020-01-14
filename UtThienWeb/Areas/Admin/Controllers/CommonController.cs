﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UtThienWeb.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Vidu()
        {
            if (Session["uName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}