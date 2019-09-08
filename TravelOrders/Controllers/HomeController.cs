using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelOrders.bl;

namespace TravelOrders.Controllers
{
    public class HomeController : MyController
    {
        public ActionResult Index()
        {
          //  ViewBag.Message = TempData["Message"];
            return View();
        }

        public ActionResult DeleteRecordsFromDatabase()
        {
            //mislim da ce bit jednostavnije da vratim partial ..
            manager.DeleteRecordsFromDatabase();
            //TempData["Message"]= "All records are deleted";
            return View("Index");
        }


    }
}