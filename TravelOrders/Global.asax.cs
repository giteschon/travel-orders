using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TravelOrders.Controllers;

namespace TravelOrders
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_EndRequest()
        {
            var statusCode = Context.Response.StatusCode;
            if (statusCode == 404 || statusCode == 500)
            {
                Response.Clear();
                var routingData = new RouteData();
                routingData.Values["controller"] = "Error";
                routingData.Values["action"] = "PageNotFound";
                routingData.Values["message"] = "PageNotFound";
                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), routingData));
            }
        }
    }
}
