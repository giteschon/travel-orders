using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOrders.Models
{
    public class TravelOrderType
    {
        public int IDTravelOrderType { get; set; }
        public string Type { get; set; }

        public TravelOrderType()
        {

        }

        public TravelOrderType(int idTravelOrderType, string type)
        {
            IDTravelOrderType = idTravelOrderType;
            Type = type;
        }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}