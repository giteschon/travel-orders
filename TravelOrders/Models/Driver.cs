using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelOrders.Models
{
    public class Driver
    {
        
        public int IDDriver { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [RegularExpression(@"^([0-9 '/-]+)$", ErrorMessage = "Only digits and / or - ")]
        public string Mobile { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]+)$", ErrorMessage = "Only digits")]
        
        public string LicenceNo { get; set; }

        public Driver()
        {

        }
        public Driver(int idDriver, string name, string surname, string mobile, string licenceNo)
        {
            IDDriver = idDriver;
            Name = name;
            Surname = surname;
            Mobile = mobile;
            LicenceNo = licenceNo;
        }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}