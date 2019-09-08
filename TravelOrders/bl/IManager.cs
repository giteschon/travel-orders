using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelOrders.Models;

namespace TravelOrders.bl
{
    public interface IManager
    {

        void AddVehicle(Vehicle vehicle);
        void EditVehicle(Vehicle vehicle);
        void DeleteVehicle(int idVehicle);
        Vehicle GetVehicle(int idVehicle);
        IEnumerable<Vehicle> GetVehicles();

        void AddDriver(Driver driver);
        void EditDriver(Driver driver);
        void DeleteDriver(int idDriver);
        Driver GetDriver(int idDriver);
        IEnumerable<Driver> GetDrivers();

        IEnumerable<Brand> GetBrands();
        Brand GetBrand(string name);

        void AddTravelOrder(TravelOrder travelOrder);
        void EditTravelOrder(TravelOrder travelOrder);
        void DeleteTravelOrder(int idTravelOrder);
        TravelOrder GetTravelOrder(int idTravelOrder);
        IEnumerable<TravelOrder> GetTravelOrders();
        IEnumerable<TravelOrder> FilterTravelOrders(int filterID);

        void DeleteRecordsFromDatabase();
        IEnumerable<TravelOrderType> GetTravelOrderTypes();
        IEnumerable<DailyAllowance> GetDailyAllowances();

        void AddRoute(Route route);
        void EditRoute(Route route);
        void DeleteRoute(int idRoute);
        IEnumerable<Route> GetRoutes();
        IEnumerable<Route> GetRoutesForTravelOrder(int idTravelOrder);
        Route GetRoute(int idRoute);

        void WriteRouteToXml(int id);

    }
}