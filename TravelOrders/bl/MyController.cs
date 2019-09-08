using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelOrders.bl
{
    public class MyController : Controller
    {
        public MyController()
        {
        }

        public TravelOrderManager manager = new TravelOrderManager();
    }
}