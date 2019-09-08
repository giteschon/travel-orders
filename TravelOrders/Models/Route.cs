using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelOrders.Models
{
    public class Route
    {
        public int IDRoute { get; set; }
                public TravelOrder TravelOrder { get; set; } 
        [Required]
        [Range(0,90)]
        public double ALatitude { get; set; }
        [Required]
        [Range(0, 180)]
        public double ALongitude { get; set; }
        [Required]
        [Range(0, 90)]
        public double BLatitude { get; set; }
        [Required]
        [Range(0, 180)]
        public double BLongitude { get; set; }
        [Required]
        [Range(10, 1000)]
        public double KilometeresSum { get; set; }
        [Required]
        [Range(40, 130)]
        public double AverageSpeed { get; set; }
        [Required]
        [Range(0, 200)]
        public double UsedFuel { get; set; }
    }
}