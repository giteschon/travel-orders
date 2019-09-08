using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelOrders.dal;
using TravelOrders.Models;

namespace TravelOrders.bl
{
    public class TravelOrderManager :IManager
    {
        IRepository repo = RepoFactory.GetRepository();
             

        #region Vehicle
        public void AddVehicle(Vehicle vehicle)
        {
            repo.AddVehicle(vehicle);
        }               

        public void DeleteVehicle(int idVehicle)
        {
            repo.DeleteVehicle(idVehicle);
        }              

        public void EditVehicle(Vehicle vehicle)
        {
            repo.EditVehicle(vehicle);
        }              

        public Vehicle GetVehicle(int idVehicle)
        {
            return repo.GetVehicle(idVehicle);
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            return repo.GetVehicles();
        }

        #endregion


        #region Driver

        public void AddDriver(Driver driver)
        {
            repo.AddDriver(driver);
        }

        public void DeleteDriver(int idDriver)
        {
            repo.DeleteDriver(idDriver);
        }

        public void EditDriver(Driver driver)
        {
            repo.EditDriver(driver);
        }

        public Driver GetDriver(int idDriver)
        {
            return repo.GetDriver(idDriver);
        }

        public IEnumerable<Driver> GetDrivers()
        {
            return repo.GetDrivers();
        }



        #endregion

        #region Brand

        public IEnumerable<Brand> GetBrands()
        {
            return repo.GetBrands();
        }

        public Brand GetBrand(string name)
        {
            return repo.GetBrand(name);
        }

        #endregion

        #region TravelOrder

        public void AddTravelOrder(TravelOrder travelOrder)
        {
            repo.AddTravelOrder(travelOrder);
        }

        public void EditTravelOrder(TravelOrder travelOrder)
        {
            repo.EditTravelOrder(travelOrder);
        }

        public void DeleteTravelOrder(int idTravelOrder)
        {
            repo.DeleteTravelOrder(idTravelOrder);
        }

        public TravelOrder GetTravelOrder(int idTravelOrder)
        {
            return repo.GetTravelOrder(idTravelOrder);
        }

        public IEnumerable<TravelOrder> GetTravelOrders()
        {
            return repo.GetTravelOrders();
        }

        public IEnumerable<TravelOrder> FilterTravelOrders(int filterID)
        {
            return repo.FilterTravelOrders(filterID);
        }
               
        #endregion

        #region Other

        public void DeleteRecordsFromDatabase()
        {
            repo.DeleteRecordsFromDatabase();
        }

        public IEnumerable<TravelOrderType> GetTravelOrderTypes()
        {
            return repo.GetTravelOrderTypes();
        }

        public IEnumerable<DailyAllowance> GetDailyAllowances()
        {
            return repo.GetDailyAllowances();
        }

       

        #endregion

        #region Route

            public void AddRoute(Route route)
        {
            repo.AddRoute(route);
        }

        public void EditRoute(Route route)
        {
            repo.EditRoute(route);
        }

        public void DeleteRoute(int idRoute)
        {
            repo.DeleteRoute(idRoute);
        }

        public IEnumerable<Route> GetRoutes()
        {
            return repo.GetRoutes();
        }

        public void WriteRouteToXml(int id)
        {
            repo.WriteRouteToXml(id);
        }

        public IEnumerable<Route> GetRoutesForTravelOrder(int idTravelOrder)
        {
            return repo.GetRoutesForTravelOrder(idTravelOrder);
        }

        public Route GetRoute(int idRoute)
        {
            return repo.GetRoute(idRoute);
        }

        #endregion

    }
}