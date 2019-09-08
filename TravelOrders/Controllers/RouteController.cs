using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelOrders.bl;
using TravelOrders.Models;

namespace TravelOrders.Controllers
{
    public class RouteController : MyController
    {
        // GET: Route
        public ActionResult GetRoutes(int idTravelOrder)
        {
            return View(manager.GetRoutes().Where(r=> r.TravelOrder.IDTravelOrder==idTravelOrder));
        }

            // GET: Route/Details/5
        public ActionResult Details(int idRoute)
        {
            return View();
        }

        // GET: Route/Create
        public ActionResult AddRoute()
        {
           
            ViewBag.travelOrder = new SelectList(manager.GetTravelOrders(), "IDTravelOrder", "TravelOrderNo");
            return View();
        }

      

        // POST: Route/Create
        [HttpPost]
        public ActionResult AddRoute(Route route)
        {
            try
            {
                int id = route.TravelOrder.IDTravelOrder;
                manager.AddRoute(route);
                return RedirectToAction("GetRoutes", new { idTravelOrder = route.TravelOrder.IDTravelOrder });
            }
            catch
            {
                return View();
            }
          

        }

        // GET: Route/Edit/5
        public ActionResult EditRoute(int idRoute)
        {
            ViewBag.travelOrder = new SelectList(manager.GetTravelOrders(), "IDTravelOrder", "TravelOrderNo");
            return View(manager.GetRoutes().Where(r=> r.IDRoute==idRoute).First());
        }

        // POST: Route/Edit/5
        [HttpPost]
        public ActionResult EditRoute(Route route)
        {
            try
            {
                manager.EditRoute(route);
                return RedirectToAction("GetRoutes", new { idTravelOrder = route.TravelOrder.IDTravelOrder });
            }
            catch
            {
                return View();
            }
            
        }

        // GET: Route/Delete/5
        public ActionResult DeleteRoute(int idRoute)
        {
            manager.DeleteRoute(idRoute);
            return RedirectToAction("GetTravelOrders","TravelOrder");
        }

      
    }
}

