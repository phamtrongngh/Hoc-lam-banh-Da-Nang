using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UtThienWeb.Controllers
{
 
    [RoutePrefix("lien-he")]
    public class ContactController : CommonController
    {
 
       [Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}