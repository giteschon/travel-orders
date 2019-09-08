using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelOrders.dal;

namespace TravelOrders.Controllers
{
    public class XmlController : Controller
    {
        private XmlHandler xml = new XmlHandler();
        SqlRepository sql = new SqlRepository();
        // GET: Xml
        public ActionResult Backup()
        {
            xml.CreateXml();
            return View();
        }

        public ActionResult Restore()
        {
            xml.ReadXml();
            return Content(xml.getTest());
        }

        public ActionResult RouteToXml(int idRoute)
        {
            sql.WriteRouteToXml(idRoute);
            return RedirectToAction("GetRoutes", "Route",new { idTravelOrder = sql.GetRoutes().Where(r=> r.IDRoute==idRoute).First().TravelOrder.IDTravelOrder });
        }

        public ActionResult ReadRoute()
        {
            sql.ReadRouteFromXml();
            return RedirectToAction("GetTravelOrders","TravelOrder");
        }
    }
}