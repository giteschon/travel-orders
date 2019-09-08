using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelOrders.Models
{
    public class Vehicle
    {

        //private const int year = DateTime.Now.Year;
        //private int year2 = 2019;
        public int IDVehicle { get; set; }
        [Required]
        public string Type { get; set; }
        public Brand Brand { get; set; }
        [Required]
        [Range(1990, 2019, ErrorMessage = "Year can be between 1990 and 2019")]
        public int YearOfProduction { get; set; }
        [Required]
        [Range(0,200000)]
        public double InitialKilometers { get; set; }
        public bool IsAvailable { get; set; }

        public Vehicle()
        {

        }

        public Vehicle(int idVehicle, String type, Brand brand, int yearOfProduction, double initalKilometeres, bool isAvailable)
        {
            IDVehicle = idVehicle;
            Type = type;
            Brand = brand;
            YearOfProduction = yearOfProduction;
            InitialKilometers = initalKilometeres;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return $"{Type}, {Brand}";
        }
    }
}