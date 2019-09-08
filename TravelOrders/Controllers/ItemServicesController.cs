using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelOrders.Models;

namespace TravelOrders.Controllers
{
    public class ItemServicesController : Controller
    {
        private TravelOrdersEntities1 db = new TravelOrdersEntities1();

        // GET: ItemServices
        public ActionResult Index(int idService)
        {
            var itemServices = db.ItemServices.Include(i => i.Service).Where(i=> i.ServiceID==idService);
            return View(itemServices.ToList());
        }

        // GET: ItemServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemService itemService = db.ItemServices.Find(id);
            if (itemService == null)
            {
                return HttpNotFound();
            }
            return View(itemService);
        }

        // GET: ItemServices/Create
        public ActionResult Create()
        {
            ViewBag.ServiceID = new SelectList(db.Services, "IDService", "IDService");
            return View();
        }

        // POST: ItemServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDItem,ServiceID,Details")] ItemService itemService)
        {
            if (ModelState.IsValid)
            {
                db.ItemServices.Add(itemService);
                db.SaveChanges();
                return RedirectToAction("Index", new { idService = itemService.ServiceID });
            }

            ViewBag.ServiceID = new SelectList(db.Services, "IDService", "IDService", itemService.ServiceID);
            return View(itemService);
        }

        // GET: ItemServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemService itemService = db.ItemServices.Find(id);
            if (itemService == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceID = new SelectList(db.Services, "IDService", "IDService", itemService.ServiceID);
            return View(itemService);
        }

        // POST: ItemServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDItem,ServiceID,Details")] ItemService itemService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { idService = itemService.ServiceID });
            }
            ViewBag.ServiceID = new SelectList(db.Services, "IDService", "IDService", itemService.ServiceID);
            return View(itemService);
        }

        // GET: ItemServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemService itemService = db.ItemServices.Find(id);
            if (itemService == null)
            {
                return HttpNotFound();
            }
            return View(itemService);
        }

        // POST: ItemServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemService itemService = db.ItemServices.Find(id);
            db.ItemServices.Remove(itemService);
            db.SaveChanges();
            //return RedirectToAction("Index", new { idService = itemService.ServiceID });
            return RedirectToAction("ViewVehicles", "Vehicles");
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
