using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelOrders.bl;
using TravelOrders.Models;

namespace TravelOrders.Controllers
{
    public class ServicesController : MyController
    {
        private TravelOrdersEntities1 db = new TravelOrdersEntities1();

               // GET: Services
        public ActionResult Index(int idVehicle)
        {
            List<Service> list = new List<Service>();
            foreach (Service service in db.Services.ToList())
            {
                if (service.VehicleID ==idVehicle)
                {
                    list.Add(service);
                }
            }
            return View(list);
        }


        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {

            ViewBag.vehicles = new SelectList(manager.GetVehicles(), "IDVehicle", "Type");
            
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDService,VehicleID,DateOfService,Price")] Service service)
        {
            if (ModelState.IsValid)
            {
                Service s = service;
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("ViewVehicles","Vehicle");
            }

            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.vehicles = new SelectList(manager.GetVehicles(), "IDVehicle", "Type");
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDService,VehicleID,DateOfService,Price")] Service service)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { idVehicle=service.VehicleID });
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            service.ItemServices.Clear();
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("ViewVehicles","Vehicles");
        }

        public ActionResult HtmlReport(int idVehicle) {
            Vehicle v = manager.GetVehicle(idVehicle);
            //total km
            double totalKm = manager.GetTravelOrders().Where(ve => ve.Vehicle.IDVehicle == idVehicle).Last().EndCounterStatus;
            //avg speed
            List<Route> routes = manager.GetRoutes().Where(r => r.TravelOrder.Vehicle.IDVehicle == idVehicle).ToList();
            double avgSpeed = 0;
            foreach (Route r in routes)
            {
                avgSpeed += r.AverageSpeed;
            }
            avgSpeed = avgSpeed / routes.Count;
            //services + items
            List<Service> services = db.Services.Where(s => s.VehicleID == idVehicle).ToList();

            //viewbags

            ViewBag.km = totalKm;
            ViewBag.avgSpeed = avgSpeed;
            ViewBag.services = services;

           




            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
