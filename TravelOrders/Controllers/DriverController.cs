using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelOrders.bl;
using TravelOrders.Models;

namespace TravelOrders.Controllers
{
    public class DriverController : MyController
    {
        // GET: Driver
        public ActionResult Index()
        {
            return View(manager.GetDrivers());
        }

        [HttpGet]
        public ActionResult AddDriver()
        {
            return View(new Driver());
        }

        [HttpPost]
        public ActionResult AddDriver(Driver model) 
        {
            //Driver d = new Driver();
            //d.Name = model.Name;
            //d.Surname = model.Surname;
            //d.Mobile = model.Mobile;
            //d.LicenceNo = model.LicenceNo;

            manager.AddDriver(model);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteDriver(Driver driver) {
            manager.DeleteDriver(driver.IDDriver);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditDriver(int idDriver)
        {
                        
            return View(manager.GetDriver(idDriver));
        }

        [HttpPost]
        public ActionResult EditDriver(Driver driver)
        {
            manager.EditDriver(driver);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult AddDriver(Driver model)
        //{
        //    //Driver d = new Driver();
        //    //d.Name = model.Name;
        //    //d.Surname = model.Surname;
        //    //d.Mobile = model.Mobile;
        //    //d.LicenceNo = model.LicenceNo;

        //    manager.AddDriver(model);

        //    return RedirectToAction("Index");
        //}
    }
}