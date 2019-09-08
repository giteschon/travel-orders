using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOrders.Models
{
    public class Fuel
    {
        public int IDFuel { get; set; }
        public Driver Driver { get; set; }
        public DateTime TimeOfPurchase { get; set; }
        public string GasStation { get; set; }
        public double Litre { get; set; }
        public double Price { get; set; }

        public Fuel()
        {

        }

        public Fuel(int idFuel, Driver driver, DateTime timeOfPurchase, string gasStation, double litre, double price)
        {
            IDFuel = IDFuel;
            Driver = driver;
            TimeOfPurchase = timeOfPurchase;
            GasStation = gasStation;
            Litre = litre;
            Price = price;
        }
    }
}