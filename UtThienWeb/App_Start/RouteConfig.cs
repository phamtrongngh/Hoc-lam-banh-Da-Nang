﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UtThienWeb.Controllers;
namespace UtThienWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();



            routes.MapRoute(
                name: "Chi Nhanh",
                url: "chi-nhanh",
                defaults: new { controller = "Chinhanh", action = "Index", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Noi Quy",
                url: "noi-quy",
                defaults: new { controller = "Lichhoc", action = "Index", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Tuyen Dung",
                url: "tuyen-dung",
                defaults: new { controller = "Tuyendung", action = "Index", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
            routes.MapRoute(
               name: "Cung hoc lam banh",
               url: "hoat-dong-hoc-vien",
               defaults: new { controller = "Hoatdonghocvien", action = "Index", id = UrlParameter.Optional },
               new[] { "UtThienWeb.Controllers" }
           );
            
            //routes.MapRoute(
            //    name: "CourseDetails",
            //    url: "chi-tiet-khoa-hoc/{name}",
            //    defaults: new { controller = "Courses", action = "CourseDetails", id = UrlParameter.Optional },
            //    new[] { "UtThienWeb.Controllers" }
            //);
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
            routes.MapRoute(
                name: "CungHocLamBanh",
                url: "cung-hoc-lam-banh",
                defaults: new { controller = "Cunghoclambanh", action = "Index", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Gio Hang",
                url: "gio-hang",
                defaults: new { controller = "Courses", action = "Cart", id = UrlParameter.Optional },
                new[] { "UtThienWeb.Controllers" }
            );
            routes.MapRoute(
                name: "DetailsShow",
                url: "{name}",
                defaults: new { controller = "Common", action = "ShowDetails", id = UrlParameter.Optional },
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
