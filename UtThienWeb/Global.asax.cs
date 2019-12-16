using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UtThienWeb.Models;

namespace UtThienWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        ModelCakes db = new ModelCakes();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
       
            var menu = db.Menus;
            foreach (var item in menu)
            {
                if (item.MenuSubLi != null)
                {
                    db.Menus.Find(item.MenuSubLi).Menu1.Add(item);
                }
            }
            HttpContext.Current.Application["menu"] = menu;
            
            HttpContext.Current.Application["course"] = db.CourseCatalogs;
            
        }
    }
}
