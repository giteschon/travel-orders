using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOrders.Models
{
    public class Brand
    {
        public int IDBrand { get; set; }
        public string Name { get; set; }

        public Brand(int idBrand, string name)
        {
            IDBrand = idBrand;
            Name = name;
        }

        public Brand()
        {

        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}