using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelOrders.bl;
using TravelOrders.Models;

namespace TravelOrders.Controllers
{
    public class VehicleController : Controller
    {
        TravelOrderManager manager = new TravelOrderManager();

        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewVehicles()
        {
            IEnumerable<Vehicle> list = manager.GetVehicles();
            return View(list);
        }

        public ActionResult DeleteVehicle(int idVehicle) {
            manager.DeleteVehicle(idVehicle);
            return RedirectToAction("ViewVehicles");
        }

        [HttpGet]
        public ActionResult EditVehicle(int idVehicle) {


            ViewBag.brands = manager.GetBrands(); 

            return View(manager.GetVehicle(idVehicle));
        }

        [HttpPost]
        public ActionResult EditVehicle(Vehicle vehicle)
        {
           vehicle.Brand.IDBrand = manager.GetBrand(vehicle.Brand.Name).IDBrand;
            
            manager.EditVehicle(vehicle);
            return RedirectToAction("ViewVehicles");
        }

        [HttpGet]
        public ActionResult AddVehicle() {

            ViewBag.brands = manager.GetBrands();
            return View();
        }

        [HttpPost]
        public ActionResult AddVehicle(Vehicle vehicle)
        {
           
            vehicle.Brand.IDBrand= manager.GetBrand(vehicle.Brand.Name).IDBrand;
            manager.AddVehicle(vehicle);
            return RedirectToAction("ViewVehicles");
        }

    }
}