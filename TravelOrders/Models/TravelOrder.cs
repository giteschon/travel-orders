using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelOrders.Models
{
    public class TravelOrder
    {
        public int IDTravelOrder { get; set; }
        [Required]
        public string TravelOrderNo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime TravelOrderDate { get; set; }
        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfDeparture { get; set; }
        [DataType(DataType.Time)]
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        [Required]
        public string DestinationStart { get; set; }
        [Required]
        public string DestinationEnd { get; set; }
        [Required]
        [Range(0, 200000)]
        public double BeginningCounterStatus { get; set; }
        [Required]
        [Range(10, 200500)]
        public double EndCounterStatus { get; set; }
        public TravelOrderType TravelOrderType { get; set; }
        public DailyAllowance DailyAllowance { get; set; }

        public TravelOrder(int id)
        {
            this.IDTravelOrder = id;
        }


        public TravelOrder()
        {

        }

        public TravelOrder(int idTravelOrder, string travelOrderNo, DateTime travelOrderDate, Driver driver, Vehicle vehicle, DateTime dateOfDeparture, string destinationStart, string destinationEnd, double beginningCounterStatus, double endCounterStatus, TravelOrderType travelOrderType, DailyAllowance dailyAllowance)
        {
            IDTravelOrder = idTravelOrder;
            TravelOrderNo = travelOrderNo;
            TravelOrderDate = travelOrderDate;
            Driver = driver;
            Vehicle = vehicle;
            DateOfDeparture = dateOfDeparture;
            DestinationStart = destinationStart;
            DestinationEnd = destinationEnd;
            BeginningCounterStatus = beginningCounterStatus;
            EndCounterStatus = endCounterStatus;
            TravelOrderType = travelOrderType;
            DailyAllowance = dailyAllowance;
        }


        public override string ToString()
        {
            return $"{TravelOrderNo}";
        }
    }
}