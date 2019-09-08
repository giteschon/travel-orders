using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TravelOrders.Models;
using System.Transactions;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using TravelOrders.Models.RoutesTableAdapters;
using System.Web.Hosting;

namespace TravelOrders.dal
{
    public class SqlRepository : IRepository
    {

        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        private const string FILEPATH = "~/route.xml";

        public SqlRepository()
        {
            Init();
        }

        #region SqlProcNames
        private const string GET_VEHICLE = "GetVehicle";
        private const string GET_VEHICLES = "GetVehicles";
        private const string ADD_VEHICLE = "AddVehicle";
        private const string EDIT_VEHICLE = "EditVehicle";
        private const string DELETE_VEHICLE = "DeleteVehicle";

        //drivers, direct sql
        private const string ADD_DRIVER = "insert into Driver values (@name,@surname,@mobile,@licenceNo)";
        private const string EDIT_DRIVER = "update Driver set [Name] = @name, Surname = @surname, Mobile = @mobile, LicenceNo = @licenceNo where IDDriver = @idDriver";
        private const string DELETE_DRIVER = "delete TravelOrder where DriverID=@idDriver " +
            "delete Driver where IDDriver=@idDriver";
        private const string GET_DRIVER = "select * from Driver where IDDriver=@idDriver";
        private const string GET_DRIVERS = "select IDDriver, Name, Surname, Mobile, LicenceNo from Driver";

        private const string GET_BRANDS = "GetBrands";
        private const string GET_BRAND = "GetBrand";
        private const string DELETE_RECORDS_FROM_DATABASE = "DeleteRecordsFromDatabase";

        private const string ADD_TRAVELORDER = "AddTravelOrder";
        private const string EDIT_TRAVELORDER = "EditTravelOrder";
        private const string DELETE_TRAVELORDER = "DeleteTravelOrder";
        private const string GET_TRAVELORDER = "GetTravelOrder";
        private const string GET_TRAVELORDERS = "GetTravelOrders";
        private const string FILTER_TRAVEL_ORDERS = "FilterTravelOrders";

        private const string GET_DAILY_ALLOWANCES = "GetDailyAllowances";
        private const string GET_TRAVEL_ORDER_TYPES = "GetTravelOrderTypes";

        //for dataset
        private const string SELECT_ROUTE = "SelectRoute";
        private const string EDIT_ROUTE = "EditRoute";
        private const string DELETE_ROUTE = "DeleteRoute";
        private const string GET_ROUTES = "GetRoutes";
        private const string GET_ROUTES_FOR_TO = "GetRoutesForTravelOrder";
        private const string ADD_ROUTE = "AddRoute";
        private const string GET_ROUTE = "GetRoute";

        #endregion

        #region Driver

        public void AddDriver(Driver driver)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        //naziv procedure
                        cmd.CommandText = ADD_DRIVER;
                        //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", driver.Name);
                        cmd.Parameters.AddWithValue("@surname", driver.Surname);
                        cmd.Parameters.AddWithValue("@mobile", driver.Mobile);
                        cmd.Parameters.AddWithValue("@licenceNo", driver.LicenceNo);


                        cmd.ExecuteNonQuery();
                        scope.Complete();

                    }
                }
            }
        }

        public void EditDriver(Driver driver)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        //naziv procedure
                        cmd.CommandText = EDIT_DRIVER;
                        cmd.Parameters.AddWithValue("@name", driver.Name);
                        cmd.Parameters.AddWithValue("@surname", driver.Surname);
                        cmd.Parameters.AddWithValue("@mobile", driver.Mobile);
                        cmd.Parameters.AddWithValue("@licenceNo", driver.LicenceNo);

                        cmd.Parameters.AddWithValue("@idDriver", driver.IDDriver);

                        cmd.ExecuteNonQuery();
                        scope.Complete();
                    }
                }
            }
        }

        public void DeleteDriver(int idDriver)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {

                        cmd.CommandText = DELETE_DRIVER;

                        cmd.Parameters.AddWithValue("@idDriver", idDriver);


                        cmd.ExecuteNonQuery();
                        scope.Complete();
                      
                    }
                }
            }

        }

        public Driver GetDriver(int idDriver)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    //naziv procedure
                    cmd.CommandText = GET_DRIVER;
                    cmd.Parameters.AddWithValue("@idDriver", idDriver);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return new Driver()
                            {
                                IDDriver = (int)r[nameof(Driver.IDDriver)],
                                Name = r[nameof(Driver.Name)].ToString(),
                                Surname = r[nameof(Driver.Surname)].ToString(),
                                Mobile = r[nameof(Driver.Mobile)].ToString(),
                                LicenceNo = r[nameof(Driver.LicenceNo)].ToString()

                            };
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Driver> GetDrivers()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GET_DRIVERS;
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            // "select IDDriver, Name, Surname, Mobile, LicenceNo from Driver";
                            Driver d = new Driver();
                            //cmd.Parameters.AddWithValue("IDDriver")

                            d.IDDriver = (int)r[nameof(Driver.IDDriver)];
                            d.Name = r[nameof(Driver.Name)].ToString();
                            d.Surname = r[nameof(Driver.Surname)].ToString();
                            d.Mobile = r[nameof(Driver.Mobile)].ToString();
                            d.LicenceNo = r[nameof(Driver.LicenceNo)].ToString();


                            yield return d;
                        }



                    }
                }
            }
        }

        #endregion

        #region Vehicle

        public void AddVehicle(Vehicle vehicle)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {

                        cmd.CommandText = ADD_VEHICLE;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@type", vehicle.Type);
                        cmd.Parameters.AddWithValue("@brandID", vehicle.Brand.IDBrand);
                        cmd.Parameters.AddWithValue("@yearOfProduction", vehicle.YearOfProduction);
                        cmd.Parameters.AddWithValue("@initialKilometers", vehicle.InitialKilometers);
                        cmd.Parameters.AddWithValue("@available", vehicle.IsAvailable);

                        cmd.ExecuteNonQuery();
                        scope.Complete();
                    }
                }
            }
        }

        public void EditVehicle(Vehicle vehicle)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {

                        cmd.CommandText = EDIT_VEHICLE;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@type", vehicle.Type);
                        cmd.Parameters.AddWithValue("@brandID", vehicle.Brand.IDBrand);
                        cmd.Parameters.AddWithValue("@yearOfProduction", vehicle.YearOfProduction);
                        cmd.Parameters.AddWithValue("@initialKilometers", vehicle.InitialKilometers);
                        cmd.Parameters.AddWithValue("@available", vehicle.IsAvailable);

                        cmd.Parameters.AddWithValue("@idVehicle", vehicle.IDVehicle);

                        cmd.ExecuteNonQuery();
                        scope.Complete();
                    }
                }
            }
        }

        public void DeleteVehicle(int idVehicle)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = DELETE_VEHICLE;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idVehicle", idVehicle);

                        cmd.ExecuteNonQuery();
                        scope.Complete();


                    }
                }
            }
        }

        public Vehicle GetVehicle(int idVehicle)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GET_VEHICLE;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idVehicle", idVehicle);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {

                            return new Vehicle()
                            {
                                IDVehicle = (int)r[nameof(Vehicle.IDVehicle)],
                                Type = r[nameof(Vehicle.Type)].ToString(),
                                Brand = new Brand()
                                {
                                    IDBrand = (int)r[nameof(Brand.IDBrand)],
                                    Name = r[nameof(Brand.Name)].ToString()
                                },
                                YearOfProduction = (int)r[nameof(Vehicle.YearOfProduction)],
                                InitialKilometers = (double)r[nameof(Vehicle.InitialKilometers)],
                                //automatski pretvori u bool
                                IsAvailable = (bool)r[nameof(Vehicle.IsAvailable)]
                        };
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Vehicle> GetVehicles()
        {

            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GET_VEHICLES;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            //yield return new Vehicle()
                            //{
                            //    IDVehicle = (int)r[nameof(Vehicle.IDVehicle)],
                            //    Type = r[nameof(Vehicle.Type)].ToString(),
                            //    Brand = new Brand()
                            //    {
                            //        Name = r[nameof(Brand.Name)].ToString()
                            //    },
                            //    YearOfProduction = (int)r[nameof(Vehicle.YearOfProduction)],
                            //    InitialKilometeres = (double)r[nameof(Vehicle.InitialKilometeres)],
                            //    //automatski pretvori u bool
                            //    IsAvailable = r.GetBoolean((int)r[nameof(Vehicle.IsAvailable)])
                            //};
                            Vehicle v = new Vehicle();

                            v.IDVehicle = (int)r[nameof(Vehicle.IDVehicle)];
                            v.Type = r[nameof(Vehicle.Type)].ToString();
                            Brand b = new Brand();
                            b.IDBrand = (int)r[nameof(Brand.IDBrand)];
                            b.Name = r[nameof(Brand.Name)].ToString();
                            v.Brand = b;
                            v.YearOfProduction = (int)r[nameof(Vehicle.YearOfProduction)];
                            v.InitialKilometers = (double)r[nameof(Vehicle.InitialKilometers)];
                            //automatski pretvori u bool
                            //v.IsAvailable = r.GetBoolean(6);
                            v.IsAvailable = (bool)r[nameof(Vehicle.IsAvailable)];

                            yield return v;
                        }



                    }
                }
            }

        }


        #endregion

        #region TravelOrder

        public void AddTravelOrder(TravelOrder travelOrder)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {

                        cmd.CommandText = ADD_TRAVELORDER;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@travelOrderNo", travelOrder.TravelOrderNo);
                        cmd.Parameters.AddWithValue("@travelOrderDate", Convert.ToDateTime(travelOrder.TravelOrderDate));
                        cmd.Parameters.AddWithValue("@driverID", travelOrder.Driver.IDDriver);
                        cmd.Parameters.AddWithValue("@vehicleID", travelOrder.Vehicle.IDVehicle);
                        cmd.Parameters.AddWithValue("@dateOfDeparture", Convert.ToDateTime(travelOrder.DateOfDeparture));
                        cmd.Parameters.AddWithValue("@destinationStart", travelOrder.DestinationStart);
                        cmd.Parameters.AddWithValue("@destinationEnd", travelOrder.DestinationEnd);
                        cmd.Parameters.AddWithValue("@beginningCounterStatus", travelOrder.BeginningCounterStatus);
                        cmd.Parameters.AddWithValue("@endCounterStatus", travelOrder.EndCounterStatus);
                        cmd.Parameters.AddWithValue("@travelOrderTypeID", travelOrder.TravelOrderType.IDTravelOrderType);
                        cmd.Parameters.AddWithValue("@dailyAllowanceID", travelOrder.DailyAllowance.IDDailyAllowance);
                        cmd.Parameters.AddWithValue("@startTime", travelOrder.StartTime);
                        cmd.Parameters.AddWithValue("@endTime", travelOrder.EndTime);


                        cmd.ExecuteNonQuery();
                        scope.Complete();
                    }
                }
            }
        }

        public void EditTravelOrder(TravelOrder travelOrder)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {

                        cmd.CommandText = EDIT_TRAVELORDER;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@travelOrderNo", travelOrder.TravelOrderNo);
                        cmd.Parameters.AddWithValue("@travelOrderDate", Convert.ToDateTime(travelOrder.TravelOrderDate));
                        cmd.Parameters.AddWithValue("@driverID", travelOrder.Driver.IDDriver);
                        cmd.Parameters.AddWithValue("@vehicleID", travelOrder.Vehicle.IDVehicle);
                        cmd.Parameters.AddWithValue("@dateOfDeparture", Convert.ToDateTime(travelOrder.DateOfDeparture));
                        cmd.Parameters.AddWithValue("@destinationStart", travelOrder.DestinationStart);
                        cmd.Parameters.AddWithValue("@destinationEnd", travelOrder.DestinationEnd);
                        cmd.Parameters.AddWithValue("@beginningCounterStatus", travelOrder.BeginningCounterStatus);
                        cmd.Parameters.AddWithValue("@endCounterStatus", travelOrder.EndCounterStatus);
                        cmd.Parameters.AddWithValue("@travelOrderTypeID", travelOrder.TravelOrderType.IDTravelOrderType);
                        cmd.Parameters.AddWithValue("@dailyAllowanceID", travelOrder.DailyAllowance.IDDailyAllowance);
                        cmd.Parameters.AddWithValue("@startTime", travelOrder.StartTime);
                        cmd.Parameters.AddWithValue("@endTime", travelOrder.EndTime);

                        cmd.Parameters.AddWithValue("@idTravelOrder", travelOrder.IDTravelOrder);

                        cmd.ExecuteNonQuery();
                        scope.Complete();
                    }
                }
            }
        }

        public void DeleteTravelOrder(int idTravelOrder)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = DELETE_TRAVELORDER;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idTravelOrder", idTravelOrder);

                        cmd.ExecuteNonQuery();
                        scope.Complete();


                    }
                }
            }
        }

        public TravelOrder GetTravelOrder(int idTravelOrder)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GET_TRAVELORDER;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idTravelOrder", idTravelOrder);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {

                            TravelOrder t = new TravelOrder();
                            t.IDTravelOrder = (int)r[nameof(TravelOrder.IDTravelOrder)];
                            t.TravelOrderNo = r[nameof(TravelOrder.TravelOrderNo)].ToString();
                            t.TravelOrderDate = Convert.ToDateTime(r[nameof(TravelOrder.TravelOrderDate)]);
                            t.DateOfDeparture = Convert.ToDateTime(r[nameof(TravelOrder.DateOfDeparture)]);
                            t.StartTime = GetSqlTime(r[nameof(TravelOrder.StartTime)].ToString());
                            t.EndTime = GetSqlTime(r[nameof(TravelOrder.EndTime)].ToString());
                            t.Driver = new Driver()
                            {
                                IDDriver = (int)r[nameof(Driver.IDDriver)],
                                Name = r[nameof(Driver.Name)].ToString(),
                                Surname = r[nameof(Driver.Surname)].ToString(),
                                Mobile = r[nameof(Driver.Mobile)].ToString(),
                                LicenceNo = r[nameof(Driver.LicenceNo)].ToString()
                            };
                            t.Vehicle = new Vehicle()
                            {
                                IDVehicle = (int)r[nameof(Vehicle.IDVehicle)],
                                Type = r[nameof(Vehicle.Type)].ToString(),
                                Brand = new Brand()
                                {
                                    IDBrand = (int)r[nameof(Brand.IDBrand)],
                                    Name = r[nameof(Brand.Name)].ToString()
                                },
                                YearOfProduction = (int)r[nameof(Vehicle.YearOfProduction)],
                                InitialKilometers = (double)r[nameof(Vehicle.InitialKilometers)],
                                IsAvailable = (bool)r[nameof(Vehicle.IsAvailable)]
                        };
                            t.DestinationStart = r[nameof(TravelOrder.DestinationStart)].ToString();
                            t.DestinationEnd = r[nameof(TravelOrder.DestinationEnd)].ToString();
                            t.BeginningCounterStatus = (double)r[nameof(TravelOrder.BeginningCounterStatus)];
                            t.EndCounterStatus = (double)r[nameof(TravelOrder.EndCounterStatus)];
                            t.TravelOrderType = new TravelOrderType()
                            {
                                IDTravelOrderType = (int)r[nameof(TravelOrderType.IDTravelOrderType)],
                                Type = r[nameof(TravelOrderType.Type)].ToString()
                            };
                            t.DailyAllowance = new DailyAllowance()
                            {
                                IDDailyAllowance = (int)r[nameof(DailyAllowance.IDDailyAllowance)],
                                DaysHoursValue = r[nameof(DailyAllowance.DaysHoursValue)].ToString(),
                                Price = (decimal)r[nameof(DailyAllowance.Price)]
                            };

                            return t;
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<TravelOrder> GetTravelOrders()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GET_TRAVELORDERS;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            TravelOrder t = new TravelOrder();
                            t.IDTravelOrder = (int)r[nameof(TravelOrder.IDTravelOrder)];
                            t.TravelOrderNo = r[nameof(TravelOrder.TravelOrderNo)].ToString();
                            t.TravelOrderDate = Convert.ToDateTime(r[nameof(TravelOrder.TravelOrderDate)]);
                            t.DateOfDeparture = Convert.ToDateTime(r[nameof(TravelOrder.DateOfDeparture)]);

                            // TimeSpan span=r.GetTimeSpan(r.GetOrdinal(r[nameof(TravelOrder.StartTime)].ToString()));
                           
                            t.StartTime = GetSqlTime(r[nameof(TravelOrder.StartTime)].ToString());
                                                        t.EndTime = GetSqlTime(r[nameof(TravelOrder.EndTime)].ToString());

                            t.Driver = new Driver()
                            {
                                IDDriver = (int)r[nameof(Driver.IDDriver)],
                                Name = r[nameof(Driver.Name)].ToString(),
                                Surname = r[nameof(Driver.Surname)].ToString(),
                                Mobile = r[nameof(Driver.Mobile)].ToString(),
                                LicenceNo = r[nameof(Driver.LicenceNo)].ToString()
                            };
                            t.Vehicle = new Vehicle()
                            {
                                IDVehicle = (int)r[nameof(Vehicle.IDVehicle)],
                                Type = r[nameof(Vehicle.Type)].ToString(),
                                Brand = new Brand()
                                {
                                    IDBrand = (int)r[nameof(Brand.IDBrand)],
                                    Name = r[nameof(Brand.Name)].ToString()
                                },
                                YearOfProduction = (int)r[nameof(Vehicle.YearOfProduction)],
                                InitialKilometers = (double)r[nameof(Vehicle.InitialKilometers)],
                                IsAvailable = (bool)r[nameof(Vehicle.IsAvailable)]
                        };
                            t.DestinationStart = r[nameof(TravelOrder.DestinationStart)].ToString();
                            t.DestinationEnd = r[nameof(TravelOrder.DestinationEnd)].ToString();
                            t.BeginningCounterStatus = (double)r[nameof(TravelOrder.BeginningCounterStatus)];
                            t.EndCounterStatus = (double)r[nameof(TravelOrder.EndCounterStatus)];
                            t.TravelOrderType = new TravelOrderType()
                            {
                                IDTravelOrderType = (int)r[nameof(TravelOrderType.IDTravelOrderType)],
                                Type = r[nameof(TravelOrderType.Type)].ToString()
                            };
                            t.DailyAllowance = new DailyAllowance()
                            {
                                IDDailyAllowance = (int)r[nameof(DailyAllowance.IDDailyAllowance)],
                                DaysHoursValue = r[nameof(DailyAllowance.DaysHoursValue)].ToString(),
                                Price = (decimal)r[nameof(DailyAllowance.Price)]
                            };

                            yield return t;

                        }



                    }



                }
            }
        }

     

        public IEnumerable<TravelOrder> FilterTravelOrders(int filterID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = FILTER_TRAVEL_ORDERS;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@filterId", filterID);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            TravelOrder t = new TravelOrder();
                            t.IDTravelOrder = (int)r[nameof(TravelOrder.IDTravelOrder)];
                            t.TravelOrderNo = r[nameof(TravelOrder.TravelOrderNo)].ToString();
                            t.TravelOrderDate = Convert.ToDateTime(r[nameof(TravelOrder.TravelOrderDate)]);
                            t.DateOfDeparture = Convert.ToDateTime(r[nameof(TravelOrder.DateOfDeparture)]);
                            t.StartTime = GetSqlTime(r[nameof(TravelOrder.StartTime)].ToString());
                            t.EndTime = GetSqlTime(r[nameof(TravelOrder.EndTime)].ToString());
                            t.Driver = new Driver()
                            {
                                IDDriver = (int)r[nameof(Driver.IDDriver)],
                                Name = r[nameof(Driver.Name)].ToString(),
                                Surname = r[nameof(Driver.Surname)].ToString(),
                                Mobile = r[nameof(Driver.Mobile)].ToString(),
                                LicenceNo = r[nameof(Driver.LicenceNo)].ToString()
                            };
                            t.Vehicle = new Vehicle()
                            {
                                IDVehicle = (int)r[nameof(Vehicle.IDVehicle)],
                                Type = r[nameof(Vehicle.Type)].ToString(),
                                Brand = new Brand()
                                {
                                    IDBrand = (int)r[nameof(Brand.IDBrand)],
                                    Name = r[nameof(Brand.Name)].ToString()
                                },
                                YearOfProduction = (int)r[nameof(Vehicle.YearOfProduction)],
                                InitialKilometers = (double)r[nameof(Vehicle.InitialKilometers)],
                                IsAvailable = (bool)r[nameof(Vehicle.IsAvailable)]
                        };
                            t.DestinationStart = r[nameof(TravelOrder.DestinationStart)].ToString();
                            t.DestinationEnd = r[nameof(TravelOrder.DestinationEnd)].ToString();
                            t.BeginningCounterStatus = (double)r[nameof(TravelOrder.BeginningCounterStatus)];
                            t.EndCounterStatus = (double)r[nameof(TravelOrder.EndCounterStatus)];
                            t.TravelOrderType = new TravelOrderType()
                            {
                                IDTravelOrderType = (int)r[nameof(TravelOrderType.IDTravelOrderType)],
                                Type = r[nameof(TravelOrderType.Type)].ToString()
                            };
                            t.DailyAllowance = new DailyAllowance()
                            {
                                IDDailyAllowance = (int)r[nameof(DailyAllowance.IDDailyAllowance)],
                                DaysHoursValue = r[nameof(DailyAllowance.DaysHoursValue)].ToString(),
                                Price = (decimal)r[nameof(DailyAllowance.Price)]
                            };

                            yield return t;

                        }
                    }
                }
            }
        }



        #endregion

        #region Brand
        public IEnumerable<Brand> GetBrands()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GET_BRANDS;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {

                            Brand b = new Brand();
                            b.IDBrand = (int)r[nameof(Brand.IDBrand)];
                            b.Name = r[nameof(Brand.Name)].ToString();


                            yield return b;
                        }



                    }
                }
            }


        }

        public Brand GetBrand(string name)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GET_BRAND;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", name);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {

                            return new Brand()
                            {
                                IDBrand = (int)r[nameof(Brand.IDBrand)],
                                Name = r[nameof(Brand.Name)].ToString()

                            };
                        }
                    }
                }
            }
            return null;
        }


        #endregion

        #region Other

        public void DeleteRecordsFromDatabase()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = DELETE_RECORDS_FROM_DATABASE;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        cmd.ExecuteNonQuery();
                        scope.Complete();


                    }
                }
            }
        }

        public IEnumerable<TravelOrderType> GetTravelOrderTypes()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GET_TRAVEL_ORDER_TYPES;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            yield return new TravelOrderType()
                            {
                                IDTravelOrderType = (int)r[nameof(TravelOrderType.IDTravelOrderType)],
                                Type = r[nameof(TravelOrderType.Type)].ToString()
                            };
                        }
                    }
                }
            }
        }

        public IEnumerable<DailyAllowance> GetDailyAllowances()
        {

            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GET_DAILY_ALLOWANCES;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            yield return new DailyAllowance()
                            {
                                IDDailyAllowance = (int)r[nameof(DailyAllowance.IDDailyAllowance)],
                                DaysHoursValue = r[nameof(DailyAllowance.DaysHoursValue)].ToString(),
                                Price = (decimal)r[nameof(DailyAllowance.Price)]
                            };
                        }
                    }
                }
            }
        }
        #endregion




        #region DataSetConnection

        private DataTable dtRoute;
        //private DataTable dtTravelOrder;

        private Routes dsDaab;
        private TravelOrderTableAdapter taTravelOrder;
        private RouteTableAdapter taRoute;

        private const string REL = "TravelOrder_RouteRelation";

        //DAAB
        private static SqlDatabase db = new SqlDatabase(cs);

        private SqlDataAdapter GetSqlAdapter()
        {
            return new SqlDataAdapter(SELECT_ROUTE, new SqlConnection(cs));
        }

        private DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            GetSqlAdapter().Fill(ds);

            dtRoute = ds.Tables[0];
            //dtTravelOrder = ds.Tables[1];

            //DataRelation rel = new DataRelation("rel", dtTravelOrder.Columns[nameof(TravelOrder.IDTravelOrder)], 
            //    dtRoute.Columns["TravelOrderID"], false);

            //ds.Relations.Add(rel);

            return ds;
        }

        #endregion

        #region Route

        public void WriteRouteToXml(int id)
        {
            //cijeli dataset
            //GetDataSet().WriteXml(HttpContext.Current.Server.MapPath("~/App_Data/"+ FILEPATH), XmlWriteMode.WriteSchema);

            DataRow row= GetDataSet().Tables[0].Select("IDRoute = " + id).First();
          

            DataSet new_ds = new DataSet("RouteData");
            
            DataTable table = GetDataSet().Tables[0].Clone();
            table.TableName = nameof(Route);
            new_ds.Tables.Add(table);
            //new_ds.Tables[0].Rows.Add(row);
            new_ds.Tables[0].ImportRow(row);

            new_ds.WriteXml(HostingEnvironment.MapPath(FILEPATH));
           
        }

        public void ReadRouteFromXml() {
            DataSet ds = new DataSet();
            ds.ReadXml(HostingEnvironment.MapPath(FILEPATH));

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    Route r = new Route();
                    string s = row[nameof(Route.IDRoute)].ToString();
                    r.IDRoute = int.Parse(row[nameof(Route.IDRoute)].ToString());
                    r.TravelOrder = GetTravelOrder(int.Parse(row["TravelOrderID"].ToString()));
                    r.ALatitude = double.Parse(row[nameof(Route.ALatitude)].ToString());
                    r.ALongitude =double.Parse(row[nameof(Route.ALongitude)].ToString());
                    r.BLatitude = double.Parse(row[nameof(Route.BLatitude)].ToString());
                    r.BLongitude = double.Parse(row[nameof(Route.BLongitude)].ToString());
                    r.AverageSpeed = double.Parse(row[nameof(Route.AverageSpeed)].ToString());
                    r.KilometeresSum = double.Parse(row[nameof(Route.KilometeresSum)].ToString());
                    r.UsedFuel = double.Parse(row[nameof(Route.UsedFuel)].ToString());

                    AddRoute(r);

                }
            }
        }


       

        private void Init() {
            dsDaab = new Routes();

            taTravelOrder = new TravelOrderTableAdapter();
            taTravelOrder.Fill(dsDaab.TravelOrder);


            taRoute = new RouteTableAdapter();
            taRoute.Fill(dsDaab.Route);
        }

        public void AddRoute(Route route)
        {
            //db.ExecuteNonQuery(ADD_ROUTE, route.TravelOrder.IDTravelOrder, route.ALatitude, route.ALongitude, route.BLatitude, route.BLongitude, route.KilometeresSum, route.AverageSpeed, route.UsedFuel);

            //db.ExecuteNonQuery(ADD_ROUTE, 1, route.ALatitude, route.ALongitude, route.BLatitude, route.BLongitude, route.KilometeresSum, route.AverageSpeed, route.UsedFuel);

           
            Routes.RouteRow routeRow = dsDaab.Route.NewRouteRow();

            routeRow.TravelOrderID = route.TravelOrder.IDTravelOrder;
            routeRow.ALatitude = route.ALatitude;
            routeRow.ALongitude = route.ALongitude;
            routeRow.BLatitude = route.BLatitude;            
            routeRow.BLongitude = route.BLongitude;
            routeRow.KilometeresSum = route.KilometeresSum;
            routeRow.AverageSpeed = route.AverageSpeed;
            routeRow.UsedFuel = route.UsedFuel;

            dsDaab.Route.Rows.Add(routeRow);
            taRoute.Update(dsDaab.Route);

        }

        public void EditRoute(Route route)
        {
           

            //db.ExecuteNonQuery(EDIT_ROUTE, route.IDRoute, route.ALatitude,route.ALongitude, route.BLatitude, route.BLongitude, route.KilometeresSum, route.AverageSpeed, route.UsedFuel);

           

            Routes.RouteRow routeRow = dsDaab.Route.Rows.Find(route.IDRoute) as Routes.RouteRow;

            
            routeRow.TravelOrderID = route.TravelOrder.IDTravelOrder;
            routeRow.ALatitude = route.ALatitude;
            routeRow.ALongitude = route.ALongitude;
            routeRow.BLatitude = route.BLatitude;
            routeRow.BLongitude = route.BLongitude;
            routeRow.KilometeresSum = route.KilometeresSum;
            routeRow.AverageSpeed = route.AverageSpeed;
            routeRow.UsedFuel = route.UsedFuel;

            taRoute.Update(dsDaab.Route);

        }

        public void DeleteRoute(int idRoute)
        {
            //DataRow drDelete = dtRoute.Rows.Find(idRoute);
            //drDelete.Delete();

            //  db.ExecuteNonQuery(DELETE_ROUTE, idRoute);

           
            Routes.RouteRow routeRow = dsDaab.Route.Rows.Find(idRoute) as Routes.RouteRow;
            routeRow.Delete();
            taRoute.Update(dsDaab.Route);


        }

        public IEnumerable<Route> GetRoutes()
        {


            //using (IDataReader dr = db.ExecuteReader(CommandType.StoredProcedure, nameof(GetRoutes)))
            //{
            //    while (dr.Read())
            //    {

            //        yield return new Route
            //        {

            //            IDRoute = (int)dr[nameof(Route.IDRoute)],
            //            //Time = (DateTime)dr[nameof(Route.Time)],
            //            ALatitude = (double)dr[nameof(Route.ALatitude)],
            //            ALongitude = (double)dr[nameof(Route.ALongitude)],
            //            BLatitude = (double)dr[nameof(Route.BLatitude)],
            //            BLongitude = (double)dr[nameof(Route.BLongitude)],
            //            KilometeresSum = (double)dr[nameof(Route.KilometeresSum)],
            //            AverageSpeed = (double)dr[nameof(Route.AverageSpeed)],
            //            UsedFuel = (double)dr[nameof(Route.UsedFuel)]

            //        };
            //    }
            //}

           

            List<Route> list = new List<Route>();
            dsDaab.Route.ToList().ForEach(row => list.Add(new Route
            {
                IDRoute = row.IDRoute,
                TravelOrder = GetTravelOrder(row.TravelOrderID),
                ALatitude = row.ALatitude,
                ALongitude = row.ALongitude,
                BLatitude = row.BLatitude,
                BLongitude = row.BLongitude,
                KilometeresSum = row.KilometeresSum,
                AverageSpeed = row.AverageSpeed,
                UsedFuel = row.UsedFuel

            }));

            return list;
        }

        public IEnumerable<Route> GetRoutesForTravelOrder(int idTravelOrder)
        {

            //DbCommand cmd = db.GetStoredProcCommand(GET_ROUTES_FOR_TO);
            //cmd.Parameters.Add(new SqlParameter("@idTravelOrder", idTravelOrder));


            //using (IDataReader dr = db.ExecuteReader(cmd))
            //    {
            //        while (dr.Read())
            //        {

            //            yield return new Route
            //            {
            //                IDRoute = (int)dr[nameof(Route.IDRoute)],
            //              //  TravelOrder = GetTravelOrder(idTravelOrder),
            //                //  Time = (DateTime)dr[nameof(Route.Time)],
            //                ALatitude = (double)dr[nameof(Route.ALatitude)],
            //                ALongitude = (double)dr[nameof(Route.ALongitude)],
            //                BLatitude = (double)dr[nameof(Route.BLatitude)],
            //                BLongitude = (double)dr[nameof(Route.BLongitude)],
            //                KilometeresSum = (double)dr[nameof(Route.KilometeresSum)],
            //                AverageSpeed = (double)dr[nameof(Route.AverageSpeed)],
            //                UsedFuel = (double)dr[nameof(Route.UsedFuel)]

            //            };
            //        }
            //    }

         

            List<Route> list = new List<Route>();
             dsDaab.Route.ToList().ForEach(row=> list.Add(new Route {
                 IDRoute=row.IDRoute,
                 TravelOrder=GetTravelOrder(row.TravelOrderID),
                 ALatitude=row.ALatitude,
                 ALongitude=row.ALongitude,
                 BLatitude=row.BLatitude,
                 BLongitude=row.BLongitude,
                 KilometeresSum=row.KilometeresSum,
                 AverageSpeed=row.AverageSpeed,
                 UsedFuel=row.UsedFuel

             }));

            return list;
        }

        public Route GetRoute(int idRoute)
        {
            DbCommand cmd = db.GetStoredProcCommand(GET_ROUTE);
            cmd.Parameters.Add(new SqlParameter("@idRoute", idRoute));

            Route r=null;

            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {

                    r= new Route
                    {
                        IDRoute = (int)dr[nameof(Route.IDRoute)],
                        //  TravelOrder = GetTravelOrder(idTravelOrder),
                        //  Time = (DateTime)dr[nameof(Route.Time)],
                        ALatitude = (double)dr[nameof(Route.ALatitude)],
                        ALongitude = (double)dr[nameof(Route.ALongitude)],
                        BLatitude = (double)dr[nameof(Route.BLatitude)],
                        BLongitude = (double)dr[nameof(Route.BLongitude)],
                        KilometeresSum = (double)dr[nameof(Route.KilometeresSum)],
                        AverageSpeed = (double)dr[nameof(Route.AverageSpeed)],
                        UsedFuel = (double)dr[nameof(Route.UsedFuel)]

                    };
                }
            }

            return r;
            
        }

        #endregion

        private DateTime GetSqlTime(string time)
        {
          
            TimeSpan span = TimeSpan.Parse(time);
            return new DateTime(span.Ticks);
        }

    }
}