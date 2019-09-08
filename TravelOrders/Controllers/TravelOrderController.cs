using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelOrders.bl;
using TravelOrders.Models;

namespace TravelOrders.Controllers
{
    public class TravelOrderController : MyController
    {
        
        public ActionResult GetTravelOrders()
        {
            ViewBag.filter = manager.GetTravelOrderTypes();
            return View(manager.GetTravelOrders());
        }

        // GET: Route/Details/5
        public ActionResult Routes(int id)
        {
            return View(manager.GetRoutesForTravelOrder(id));
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult AddTravelOrder()
        {
            ViewBag.drivers =  new SelectList(manager.GetDrivers(), "IDDriver", "Surname");
            ViewBag.vehicles = new SelectList(manager.GetVehicles(), "IDVehicle", "Type");
            ViewBag.toType = new SelectList(manager.GetTravelOrderTypes(), "IDTravelOrderType", "Type");
            ViewBag.allowance = new SelectList(manager.GetDailyAllowances(), "IDDailyAllowance", "DaysHoursValue");
            ViewBag.brands = new SelectList(manager.GetBrands(), "IDBrand", "Name");
            return View();
        }

       
        [HttpPost]
        public ActionResult AddTravelOrder(TravelOrder travelOrder)
        {
           
            manager.AddTravelOrder(travelOrder);
            return RedirectToAction("GetTravelOrders");
        }

        
        public ActionResult EditTravelOrder(int idTravelOrder)
        {
            ViewBag.drivers = manager.GetDrivers();
            ViewBag.vehicles = manager.GetVehicles();
            ViewBag.toType = manager.GetTravelOrderTypes();
            ViewBag.allowance = manager.GetDailyAllowances();
            //ViewBag.travelOrderType = manager.GetVehicles();
            //ViewBag.dailyAllowance = manager.GetVehicles();

            return View(manager.GetTravelOrder(idTravelOrder));

            
        }

      
        [HttpPost]
        public ActionResult EditTravelOrder(TravelOrder travelOrder)
        {
           
            manager.EditTravelOrder(travelOrder);
            return RedirectToAction("GetTravelOrders");
        }

        
        public ActionResult DeleteTravelOrder(int idTravelOrder)
        {
            manager.DeleteTravelOrder(idTravelOrder);
            return RedirectToAction("GetTravelOrders");
        }


        public ActionResult FilterFuture()
        {
         
            return View(manager.FilterTravelOrders(3));
        }

        public ActionResult FilterOpened()
        {

            return View(manager.FilterTravelOrders(2));
        }

        public ActionResult FilterClosed()
        {

            return View(manager.FilterTravelOrders(1));
        }

    }
}
