using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Hosting;
using TravelOrders.Models;
using TravelOrders.bl;
using System.Data;
using System.Globalization;

namespace TravelOrders.dal
{
    public class XmlHandler
    {
        private const string PATH = "~/backup.xml";
        private IManager manager = new TravelOrderManager();
        private TravelOrdersEntities1 db = new TravelOrdersEntities1();

        #region Backup

        public void CreateXml()
        {
            using (XmlWriter writer = CreateWriter())
            {
                //root
                writer.WriteStartElement("DbData");
                //not changable data...
                //WriteBrands(writer);
                //ovo se ne bi smjelo mjenjat zbod id-a ...

                //user changable data
                WriteDrivers(writer);
                WriteVehicles(writer);
                WriteTravelOrder(writer);
                WriteRoute(writer);
                WriteServices(writer);
                WriteItemsService(writer);


                //root end
                writer.WriteEndElement();
            }
            //!!
            db.Dispose();

            //ClearDb();
        }

        private XmlWriter CreateWriter()
        {
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true
            };
            //map on the server
            return XmlWriter.Create(HostingEnvironment.MapPath(PATH), settings);
        }

        #region WriterChildElements

        private void WriteBrands(XmlWriter writer)
        {
            writer.WriteStartElement("Brands");
            foreach (Brand brand in manager.GetBrands())
            {
                writer.WriteStartElement(nameof(Brand));
                //att => id
                writer.WriteAttributeString(nameof(Brand.IDBrand), brand.IDBrand.ToString());
                writer.WriteElementString(nameof(Brand.Name), brand.Name);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

        }

        private void WriteDrivers(XmlWriter writer)
        {
            writer.WriteStartElement("Drivers");
            foreach (Driver driver in manager.GetDrivers())
            {
                writer.WriteStartElement(nameof(Driver));
                writer.WriteAttributeString(nameof(Driver.IDDriver), driver.IDDriver.ToString());
                writer.WriteElementString(nameof(Driver.Name), driver.Name);
                writer.WriteElementString(nameof(Driver.Surname), driver.Surname);
                writer.WriteElementString(nameof(Driver.Mobile), driver.Mobile);
                writer.WriteElementString(nameof(Driver.LicenceNo), driver.LicenceNo);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteTravelOrder(XmlWriter writer)
        {
            writer.WriteStartElement("TravelOrders");
            foreach (TravelOrder to in manager.GetTravelOrders())
            {
                writer.WriteStartElement(nameof(TravelOrder));
                writer.WriteAttributeString(nameof(TravelOrder.IDTravelOrder), to.IDTravelOrder.ToString());
                writer.WriteElementString(nameof(TravelOrder.TravelOrderNo), to.TravelOrderNo);
                writer.WriteElementString(nameof(TravelOrder.TravelOrderDate), to.TravelOrderDate.ToString("yyyy-MM-dd"));
                writer.WriteElementString(nameof(TravelOrder.Driver.IDDriver), to.Driver.IDDriver.ToString());
                writer.WriteElementString(nameof(TravelOrder.Vehicle.IDVehicle), to.Vehicle.IDVehicle.ToString());
                writer.WriteElementString(nameof(TravelOrder.DateOfDeparture), to.DateOfDeparture.ToString("yyyy-MM-dd"));
                //writer.WriteElementString(nameof(TravelOrder.DateOfDeparture), XmlConvert.ToString(value: to.DateOfDeparture));
                writer.WriteElementString(nameof(TravelOrder.StartTime), to.StartTime.ToString("HH:mm:ss"));
                writer.WriteElementString(nameof(TravelOrder.EndTime), to.EndTime.ToString("HH:mm:ss"));
                //writer.WriteElementString(nameof(TravelOrder.StartTime), XmlConvert.ToString(value: to.StartTime));
                //writer.WriteElementString(nameof(TravelOrder.EndTime), XmlConvert.ToString(value: to.EndTime));
                writer.WriteElementString(nameof(TravelOrder.DestinationStart), to.DestinationStart);
                writer.WriteElementString(nameof(TravelOrder.DestinationEnd), to.DestinationEnd);
                writer.WriteElementString(nameof(TravelOrder.BeginningCounterStatus), to.BeginningCounterStatus.ToString());
                writer.WriteElementString(nameof(TravelOrder.EndCounterStatus), to.EndCounterStatus.ToString());
                writer.WriteElementString(nameof(TravelOrder.TravelOrderType.IDTravelOrderType), to.TravelOrderType.IDTravelOrderType.ToString());
                writer.WriteElementString(nameof(TravelOrder.DailyAllowance.IDDailyAllowance), to.DailyAllowance.IDDailyAllowance.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteVehicles(XmlWriter writer)
        {
            writer.WriteStartElement("Vehicles");
            foreach (Vehicle vehicle in manager.GetVehicles())
            {
                writer.WriteStartElement(nameof(Vehicle));
                writer.WriteAttributeString(nameof(Vehicle.IDVehicle), vehicle.IDVehicle.ToString());
                writer.WriteElementString(nameof(Vehicle.Type), vehicle.Type);
                writer.WriteElementString(nameof(Vehicle.Brand.IDBrand), vehicle.Brand.IDBrand.ToString());
                writer.WriteElementString(nameof(Vehicle.YearOfProduction), vehicle.YearOfProduction.ToString());
                writer.WriteElementString(nameof(Vehicle.InitialKilometers), vehicle.InitialKilometers.ToString());
                //bool can only be auto converted if it's in lower case
                writer.WriteElementString(nameof(Vehicle.IsAvailable), vehicle.IsAvailable.ToString().ToLower());

                writer.WriteEndElement();

            }
            writer.WriteEndElement();
        }

        private void WriteRoute(XmlWriter writer)
        {
            writer.WriteStartElement("Routes");
            foreach (Route route in manager.GetRoutes())
            {
                writer.WriteStartElement(nameof(Route));
                writer.WriteAttributeString(nameof(Route.IDRoute), route.IDRoute.ToString());
                writer.WriteElementString(nameof(Route.TravelOrder.IDTravelOrder), route.TravelOrder.IDTravelOrder.ToString());
                writer.WriteElementString(nameof(Route.ALatitude), route.ALatitude.ToString());
                writer.WriteElementString(nameof(Route.ALongitude), route.ALongitude.ToString());
                writer.WriteElementString(nameof(Route.BLatitude), route.BLatitude.ToString());
                writer.WriteElementString(nameof(Route.BLongitude), route.BLongitude.ToString());
                writer.WriteElementString(nameof(Route.KilometeresSum), route.KilometeresSum.ToString());
                writer.WriteElementString(nameof(Route.AverageSpeed), route.AverageSpeed.ToString());
                writer.WriteElementString(nameof(Route.UsedFuel), route.UsedFuel.ToString());

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteItemsService(XmlWriter writer)
        {
            writer.WriteStartElement("Items");
            foreach (ItemService item in db.ItemServices.ToList())
            {
                writer.WriteStartElement(nameof(ItemService));
                writer.WriteAttributeString(nameof(ItemService.IDItem), item.IDItem.ToString());
                writer.WriteElementString(nameof(ItemService.ServiceID), item.ServiceID.ToString());
                writer.WriteElementString(nameof(ItemService.Details), item.Details);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteServices(XmlWriter writer)
        {
            writer.WriteStartElement("Services");
            foreach (Service service in db.Services.ToList())
            {
                writer.WriteStartElement(nameof(Service));
                writer.WriteAttributeString(nameof(Service.IDService), service.IDService.ToString());
                writer.WriteElementString(nameof(Service.VehicleID), service.VehicleID.ToString());
                //nullable date, getvalue makes it regular datetime
                //testing for null is not neccessary -> everything is required
                //  writer.WriteElementString(nameof(Service.DateOfService), service.DateOfService.GetValueOrDefault().ToShortDateString());
                writer.WriteElementString(nameof(Service.DateOfService), service.DateOfService.GetValueOrDefault().ToString("yyyy-MM-dd"));
                writer.WriteElementString(nameof(Service.Price), service.Price.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        #endregion

        #endregion

     

        #region Restore

        public void ReadXml()
        {

            db = new TravelOrdersEntities1();
            using (XmlReader reader = CreaterReader())
            {
                
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                                switch (reader.Name)
                                {
                                    case nameof(Driver):
                                ReadDriver(reader);
                                        break;
                            case nameof(Vehicle):
                                ReadVehicle(reader);
                                break;
                            case nameof(TravelOrder):
                                ReadTravelOrder(reader);
                                break;
                            case nameof(Route):
                                ReadRoutes(reader);
                                break;
                            case nameof(Service):
                               ReadService(reader);
                                break;
                            case nameof(ItemService):
                                ReadItems(reader);
                                break;
                        }
                          
                    }
                }
            }
            db.Dispose();
        }

        private string test = "";
        public string getTest() {

            return test;
        }
            

        private XmlReader CreaterReader()
        {
            return XmlReader.Create(HostingEnvironment.MapPath(PATH));
        }

        #region ReadMethods

        private void ReadRoutes(XmlReader reader)
        {
            Route route = new Route();
            route.IDRoute= int.Parse(reader.GetAttribute(nameof(Route.IDRoute)));

            if (reader.ReadToFollowing(nameof(Route.TravelOrder.IDTravelOrder)))
                 route.TravelOrder= manager.GetTravelOrder(reader.ReadElementContentAsInt());

            if (reader.ReadToFollowing(nameof(Route.ALatitude)))
            {
                route.ALatitude= reader.ReadElementContentAsDouble();
            }

            if (reader.ReadToFollowing(nameof(Route.ALongitude)))
            {
                route.ALongitude = reader.ReadElementContentAsDouble();
            }

            if (reader.ReadToFollowing(nameof(Route.BLatitude)))
            {
                route.BLatitude = reader.ReadElementContentAsDouble();
            }

            if (reader.ReadToFollowing(nameof(Route.BLongitude)))
            {
                route.BLongitude = reader.ReadElementContentAsDouble();
            }

            if (reader.ReadToFollowing(nameof(Route.KilometeresSum)))
            {
                route.KilometeresSum = reader.ReadElementContentAsDouble();
            }

            if (reader.ReadToFollowing(nameof(Route.AverageSpeed)))
            {
                route.AverageSpeed = reader.ReadElementContentAsDouble();
            }

            if (reader.ReadToFollowing(nameof(Route.UsedFuel)))
            {
                route.UsedFuel = reader.ReadElementContentAsDouble();
            }


            manager.AddRoute(route);
        }

        private void ReadItems(XmlReader reader)
        {
            ItemService item = new ItemService();
           item.IDItem = int.Parse(reader.GetAttribute(nameof(ItemService.IDItem)));

            if (reader.ReadToFollowing(nameof(ItemService.ServiceID)))
               item.ServiceID  = reader.ReadElementContentAsInt();

            if (reader.ReadToFollowing(nameof(ItemService.Details)))
                item.Details = reader.ReadElementContentAsString();

            db.ItemServices.Add(item);
            db.SaveChanges();
        }

        private void ReadService(XmlReader reader)
        {
            Service service = new Service();
            service.IDService= int.Parse(reader.GetAttribute(nameof(Service.IDService)));

            if (reader.ReadToFollowing(nameof(Service.VehicleID)))
                 service.VehicleID= reader.ReadElementContentAsInt();

            if (reader.ReadToFollowing(nameof(Service.DateOfService)))
            {
                service.DateOfService= reader.ReadElementContentAsDateTime();
            }

            if (reader.ReadToFollowing(nameof(Service.Price)))
            {
                 service.Price= reader.ReadElementContentAsDecimal();
            }
            db.Services.Add(service);
        }

        private void ReadTravelOrder(XmlReader reader)
        {
            TravelOrder to = new TravelOrder();
            to.IDTravelOrder = int.Parse(reader.GetAttribute(nameof(TravelOrder.IDTravelOrder)));

            if (reader.ReadToFollowing(nameof(TravelOrder.TravelOrderNo)))
                to.TravelOrderNo = reader.ReadElementContentAsString();

            if (reader.ReadToFollowing(nameof(TravelOrder.Driver.IDDriver)))
            {
                to.Driver = manager.GetDriver(reader.ReadElementContentAsInt());
            }

            if (reader.ReadToFollowing(nameof(TravelOrder.Vehicle.IDVehicle)))
            {
                to.Vehicle = manager.GetVehicle(reader.ReadElementContentAsInt());
            }

            DateTime date= new DateTime();
            if (reader.ReadToFollowing(nameof(TravelOrder.TravelOrderDate)))
            {
                //   to.TravelOrderDate = DateTime.ParseExact(reader.ReadElementContentAsString(), "dd/MM/yyyy",null);
                // to.TravelOrderDate = reader.ReadElementContentAsDateTime();
                 date = XmlConvert.ToDateTime(reader.ReadElementContentAsString());
                //to.TravelOrderDate =DateTime.TryParse(reader.ReadElementContentAsString(), ;
            }

                if (reader.ReadToFollowing(nameof(TravelOrder.DateOfDeparture)))
                {
                //  to.DateOfDeparture = DateTime.ParseExact(reader.ReadElementContentAsString(), "dd/MM/yyyy", null);
                to.DateOfDeparture = reader.ReadElementContentAsDateTime();
            }
                if (reader.ReadToFollowing(nameof(TravelOrder.StartTime)))
                {
                // to.StartTime = DateTime.ParseExact(reader.ReadElementContentAsString(), "HH:mm", CultureInfo.InvariantCulture);
                to.StartTime = reader.ReadElementContentAsDateTime();
            }
                if (reader.ReadToFollowing(nameof(TravelOrder.EndTime)))
                {
                // to.EndTime = DateTime.ParseExact(reader.ReadElementContentAsString(), "HH:mm", CultureInfo.InvariantCulture);
                to.EndTime = reader.ReadElementContentAsDateTime();
            }
                if (reader.ReadToFollowing(nameof(TravelOrder.DestinationStart)))
                {
                    to.DestinationStart = reader.ReadElementContentAsString();
                }
                if (reader.ReadToFollowing(nameof(TravelOrder.DestinationEnd)))
                {
                    to.DestinationEnd = reader.ReadElementContentAsString();
                }
                if (reader.ReadToFollowing(nameof(TravelOrder.BeginningCounterStatus)))
                {
                    to.BeginningCounterStatus = reader.ReadElementContentAsDouble();
                }
                if (reader.ReadToFollowing(nameof(TravelOrder.EndCounterStatus)))
                {
                    to.EndCounterStatus = reader.ReadElementContentAsDouble();
                }

                TravelOrderType type = new TravelOrderType();
                if (reader.ReadToFollowing(nameof(TravelOrder.TravelOrderType.IDTravelOrderType)))
                {
                    int id = reader.ReadElementContentAsInt();
                    type = manager.GetTravelOrderTypes().Where(t => t.IDTravelOrderType == id).First();
                }
                DailyAllowance dailyAllowance = new DailyAllowance();
                if (reader.ReadToFollowing(nameof(TravelOrder.DailyAllowance.IDDailyAllowance)))
                {
                    int id = reader.ReadElementContentAsInt();
                    dailyAllowance = manager.GetDailyAllowances().Where(t => t.IDDailyAllowance == id).First();
                }

                to.TravelOrderType = type;
                to.DailyAllowance = dailyAllowance;
            to.TravelOrderDate = new DateTime(date.Year, date.Month, date.Day);
                manager.AddTravelOrder(to);
            
        }

        private void ReadVehicle(XmlReader reader)
        {
            Vehicle v = new Vehicle();  
            v.IDVehicle = int.Parse(reader.GetAttribute(nameof(Vehicle.IDVehicle)));

            if (reader.ReadToFollowing(nameof(Vehicle.Type)))
                 v.Type= reader.ReadElementContentAsString();

            if (reader.ReadToFollowing(nameof(Vehicle.Brand.IDBrand)))
            {
                int id = reader.ReadElementContentAsInt();
                v.Brand =manager.GetBrands().Where(b=> b.IDBrand == id ).First();
            }

            if (reader.ReadToFollowing(nameof(Vehicle.YearOfProduction)))
            {
                 v.YearOfProduction= reader.ReadElementContentAsInt();
            }

            if (reader.ReadToFollowing(nameof(Vehicle.InitialKilometers)))
            {
                v.InitialKilometers= reader.ReadElementContentAsDouble();
            }

            if (reader.ReadToFollowing(nameof(Vehicle.IsAvailable)))
            {
                v.IsAvailable = reader.ReadElementContentAsBoolean();
            }

            manager.AddVehicle(v);
        }

        private void ReadDriver(XmlReader reader)
        {
            Driver d = new Driver();
            d.IDDriver = int.Parse(reader.GetAttribute(nameof(Driver.IDDriver)));

            if (reader.ReadToFollowing(nameof(Driver.Name)))
                d.Name = reader.ReadElementContentAsString();

            if (reader.ReadToFollowing(nameof(Driver.Surname)))
            {
                d.Surname = reader.ReadElementContentAsString();
            }

            if (reader.ReadToFollowing(nameof(Driver.Mobile)))
            {
                d.Mobile = reader.ReadElementContentAsString();
            }

            if (reader.ReadToFollowing(nameof(Driver.LicenceNo)))
            {
                d.LicenceNo = reader.ReadElementContentAsString();
            }

            manager.AddDriver(d);

        }

        #endregion

        #endregion



        public string ShowXml() {
            XmlDocument doc = new XmlDocument();
            doc.Load(PATH);
            string xml = "";
            doc.Save(xml);

            return xml;
        }

        private void ClearDb()
        {
            manager.DeleteRecordsFromDatabase();

        }
    }
}