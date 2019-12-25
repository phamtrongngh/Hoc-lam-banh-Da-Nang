using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    public class CoursesNewsRouteHandler : MvcRouteHandler
    {
        ModelCakes db = new ModelCakes();
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string name = requestContext.RouteData.Values["name"] as string;
            name = HomeController.RemoveUnicode(name);
            Course course = db.Courses.SingleOrDefault(a => a.CourseName.Equals(name));
            if (course != null)
            {
                requestContext.RouteData.Values["controller"] = "Courses";
                requestContext.RouteData.Values["action"] = "CourseDetails";
            }
            else
            {
                News news = db.News.SingleOrDefault(a => a.NewsTitle.Equals(name));
                if (news != null)
                {
                    requestContext.RouteData.Values["controller"] = "News";
                    requestContext.RouteData.Values["action"] = "Details";
                }
            }
            return base.GetHttpHandler(requestContext);
        }
    }
}