using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOrders.Models
{
    public class DailyAllowance
    {
        public int IDDailyAllowance { get; set; }
        public string DaysHoursValue { get; set; }
        public decimal Price { get; set; }

        public DailyAllowance(int idDailyAllowance, string daysHoursValue, decimal price)
        {
            IDDailyAllowance = idDailyAllowance;
            DaysHoursValue = daysHoursValue;
            Price = price;
        }
        public DailyAllowance()
        {

        }

        public override string ToString()
        {
            return $"{Price}";
        }
    }
}