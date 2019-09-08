using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelOrders.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult PageNotFound(string msg)
        {
            ViewBag.message = msg;
            return View();
        }
    }
}