using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UtThienWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "CourseItem",
            //    url: "{name}-{id}",
            //    defaults: new { controller = "Courses", action = "CoursesItem", id = UrlParameter.Optional },
            //    new[] { "UtThienWeb.Controllers" }
            //);
            routes.MapRoute(
                name: "CourseItem",
                url: "khoa-hoc/{name}",
                defaults: new { controller = "Courses", action = "CoursesItem", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
            routes.MapRoute(
                name: "CourseDetails",
                url: "chi-tiet-khoa-hoc/{name}",
                defaults: new { controller = "Courses", action = "CourseDetails", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
        }
    }
}
