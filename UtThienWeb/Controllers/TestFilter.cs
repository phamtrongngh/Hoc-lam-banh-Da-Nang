using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UtThienWeb.Models;
namespace UtThienWeb.Controllers
{
    public class TestFilter : ActionFilterAttribute
    {
        ModelCakes db = new ModelCakes();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting ", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var menu = db.Menus;
        }
    }
}