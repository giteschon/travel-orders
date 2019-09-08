CREATE DATABASE TravelOrders
go
use TravelOrders
go

--use master drop database TravelOrders

create table Driver
(
IDDriver int primary key identity,
[Name] nvarchar(50),
Surname nvarchar(50),
Mobile nvarchar(25),
LicenceNo nvarchar(8)
)

create table Brand
(
IDBrand int primary key identity,
[Name] nvarchar(50)
)

create table Vehicle
(
IDVehicle int primary key identity,
[Type] nvarchar (50),
BrandID int foreign key references Brand(IDBrand),
YearOfProduction int,
InitialKilometers float,
--provjera je li na servisu ili se koristi
IsAvailable bit default 1
)

create table TravelOrderType
(
IDTravelOrderType int primary key identity,
[Type] nvarchar(50)
)



create table DailyAllowance
(
IDDailyAllowance int primary key identity,
--broj dana odnosno sati po kojima se racuna dnevnica
DaysHoursValue nvarchar(10),
Price money
)

create table TravelOrder
(
IDTravelOrder int primary key identity,
TravelOrderNo nvarchar(10),
TravelOrderDate date,
DriverID int foreign key references Driver(IDDriver),
VehicleID int foreign key references Vehicle(IDVehicle),
DateOfDeparture date,
StartTime time,
EndTime time,
DestinationStart nvarchar(50),
DestinationEnd nvarchar(50),
BeginningCounterStatus float,
EndCounterStatus float,
--GPSTrackingID int foreign key references GPSTracking(IDGPSTracking)
TravelOrderTypeID int foreign key references TravelOrderType(IDTravelOrderType),
DailyAllowanceID int foreign key references DailyAllowance(IDDailyAllowance)
)

create table Fuel
(
IDFuel int primary key identity,
DriverID int foreign key references Driver(IDDriver),
TravelOrderID int foreign key references TravelOrder(IDTravelOrder),
TimeOfPurchase datetime,
GasStation nvarchar(100),
Litre float,
Price money
)


--mora se i vratit natrag tako da putni nalog ide na gps jer ih onda moze bit vise 
create table GPSTracking
(
IDGPSTracking int primary key identity,
TravelOrderID int foreign key references TravelOrder(IDTravelOrder),
[Time] time,
--kordinate za mjesto A i B -> pretpostavljam pocetak i zavrsetak putovanja
ALatitude float,
ALongitude float,
BLatitude float,
BLongitude float,
KilometeresSum float,
AverageSpeed float,
--ne znam kaj s tim
UsedFuel float
)

create table [Service]
(
IDService int primary key identity,
VehicleID int foreign key references Vehicle(IDVehicle),
DateOfService date,
Price money
)

create table ItemService
(
IDItem int primary key identity,
ServiceID int foreign key references [Service](IDService),
Details nvarchar(300)
)

create table [Route]
(
IDRoute int primary key identity,
TravelOrderID int foreign key references TravelOrder(IDTravelOrder),
ALatitude float,
ALongitude float,
BLatitude float,
BLongitude float,
KilometeresSum float,
AverageSpeed float,
UsedFuel float
)


go
--DATA--
insert into TravelOrderType values
('Closed'),('Open'), ('Future'), ('Filtered')

insert into DailyAllowance values
('<8h',0), ('8-12h',85),('12-24h',170), ('2d',340), ('3d',510)

insert into Brand values
('BMW'), ('Mercedes-Benz'), ('Volkswagen'), ('Volvo'), ('Audi'), ('Nissan'), ('Toyota')

--PROCEDURE 

--DRIVER - NEMAJU STORANE

--VEHICLE
--get
go
create proc GetVehicle
@idVehicle int
as
select v.IDVehicle, v.[Type],b.IDBrand, b.[Name], v.YearOfProduction, v.InitialKilometers, v.IsAvailable
from Vehicle as v
inner join Brand as b
on b.IDBrand=v.BrandID
where v.IDVehicle = @idVehicle

go
create proc GetVehicles
as
select v.IDVehicle, v.[Type], b.IDBrand,b.[Name], v.YearOfProduction, v.InitialKilometers, v.IsAvailable
from Vehicle as v
inner join Brand as b
on b.IDBrand=v.BrandID



--crud -insert,update,delete
go
create proc AddVehicle
@type nvarchar(50),
@brandID int,
@yearOfProduction int,
@initialKilometers float,
@available bit
as
insert into Vehicle values
(@type,@brandID,@yearOfProduction,@initialKilometers,@available)




go
create proc EditVehicle
@type nvarchar(50),
@brandID int,
@yearOfProduction int,
@initialKilometers float,
@available bit,
@idVehicle int
as
update Vehicle set
[Type]=@type,
BrandID=@brandID,
YearOfProduction=@yearOfProduction,
InitialKilometers=@initialKilometers,
IsAvailable=@available
where IDVehicle=@idVehicle

go
create proc DeleteVehicle
@idVehicle int
as
begin
delete TravelOrder where VehicleID=@idVehicle
delete Vehicle where IDVehicle=@idVehicle
end

--TRAVEL ORDER

--CRUD  insert, update,delete
go

create proc GetTravelOrder
@idTravelOrder int
as
select * from TravelOrder  as t
inner join Driver as d
on d.IDDriver=t.DriverID
inner join Vehicle as v
on v.IDVehicle=t.VehicleID
inner join Brand as b
on b.IDBrand=v.BrandID
inner join TravelOrderType as tt
on tt.IDTravelOrderType=t.TravelOrderTypeID
inner join DailyAllowance as da
on da.IDDailyAllowance=t.DailyAllowanceID
where IDTravelOrder=@idTravelOrder

go
create proc GetTravelOrders
as
select * from TravelOrder  as t
inner join Driver as d
on d.IDDriver=t.DriverID
inner join Vehicle as v
on v.IDVehicle=t.VehicleID
inner join Brand as b
on b.IDBrand=v.BrandID
inner join TravelOrderType as tt
on tt.IDTravelOrderType=t.TravelOrderTypeID
inner join DailyAllowance as da
on da.IDDailyAllowance=t.DailyAllowanceID

go


create proc AddTravelOrder
@travelOrderNo nvarchar(10),
@travelOrderDate date,
@driverId int,
@vehicleId int,
@dateOfDeparture date,
@startTime time,
@endTime time,
@destinationStart nvarchar(50),
@destinationEnd nvarchar(50),
@beginningCounterStatus float,
@endCounterStatus float,
@travelOrderTypeId int,
@dailyAllowanceId int
as
insert into TravelOrder values
(@travelOrderNo, @travelOrderDate, @driverId,@vehicleId,@dateOfDeparture,@startTime,@endTime, @destinationStart, @destinationEnd, @beginningCounterStatus, @endCounterStatus, @travelOrderTypeId, @dailyAllowanceId)


go
create proc EditTravelOrder
@travelOrderNo nvarchar(10),
@travelOrderDate date,
@driverId int,
@vehicleId int,
@dateOfDeparture date,
@startTime time,
@endTime time,
@destinationStart nvarchar(50),
@destinationEnd nvarchar(50),
@beginningCounterStatus float,
@endCounterStatus float,
@travelOrderTypeId int,
@dailyAllowanceId int,
@idTravelOrder int
as
update TravelOrder set
TravelOrderNo=@travelOrderNo,
TravelOrderDate=@travelOrderDate,
DriverID=@driverId,
VehicleID=@vehicleId,
DateOfDeparture=@dateOfDeparture,
StartTime=@startTime,
EndTime=@endTime,
DestinationStart=@destinationStart,
DestinationEnd=@destinationEnd,
BeginningCounterStatus=@beginningCounterStatus,
EndCounterStatus=@endCounterStatus,
TravelOrderTypeID=@travelOrderTypeId,
DailyAllowanceID=@dailyAllowanceId
where IDTravelOrder=@idTravelOrder


go
create proc DeleteTravelOrder
@idTravelOrder int
as
delete TravelOrder where IDTravelOrder=@idTravelOrder

go
create proc FilterTravelOrders
@filterId int
as
select * from TravelOrder  as t
inner join Driver as d
on d.IDDriver=t.DriverID
inner join Vehicle as v
on v.IDVehicle=t.VehicleID
inner join Brand as b
on b.IDBrand=v.BrandID
inner join TravelOrderType as tt
on tt.IDTravelOrderType=t.TravelOrderTypeID
inner join DailyAllowance as da
on da.IDDailyAllowance=t.DailyAllowanceID
where TravelOrderTypeID=@filterId


----drivers direct sql
--insert into Driver values ('ke','ke','ke','ke')

--update Driver set
--[Name]=@name,
--Surname=@surname,
--Mobile=@mobile,
--LicenceNo=@licenceNo
--where IDDriver=@idDriver


----ovo ne bi trebalo radit ako imamo putne naloge
--delete Driver where IDDriver=@idDriver

--setYea

--select * from Driver where IDDriver=@idDriver

go
create proc GetBrands
as
select * from Brand

go
create proc GetBrand
@name nvarchar(50)
as
select * from Brand where [Name]=@name

go
create proc DeleteRecordsFromDatabase
as
begin
delete [Route]
delete ItemService
delete [Service]
delete TravelOrder


delete Vehicle
delete Driver
end

--drop proc DeleteRecordsFromDatabase


go
create proc GetDailyAllowances
as
select * from DailyAllowance

go
create proc GetTravelOrderTypes
as
select * from TravelOrderType

go
create proc AddService
@vehicleId int,
@date date,
@price money
as
insert into [Service] values
(@vehicleId, @date,@price)
go

create proc EditService
@vehicleId int,
@date date,
@price money,
@id int
as
update [Service] set
VehicleID=@vehicleId,
DateOfService=@date,
Price=@price
where IDService=@id
go

create proc GetServicesPerVehicle
@idVehcle int
as
select * from [Service] where VehicleID=@idVehcle
go

create proc GetService
@id int
as
select * from [Service] where IDService=@id
go

create proc DeleteService
@id int
as
delete ItemService where ServiceID=@id
delete [Service] where IDService=@id
go

create proc AddItemService
@serviceId int,
@details nvarchar(300)
as
insert into ItemService values
(@serviceId, @details)
go

create proc EditItemService
@serviceId int,
@details nvarchar(300),
@id int
as
update ItemService set
ServiceID=@serviceId,
Details=@details
where IDItem=@id
go

create proc DeleteItem
@id int
as
delete ItemService where IDItem=@id
go

create proc GetItem
@id int
as
select * from ItemService where IDItem=@id
go

create proc GetItemsPerService
@idService int
as
select * from ItemService where ServiceID=@idService
go

create proc SelectRoute
as
select * from [Route]
select * from [TravelOrder]
go

--DUMMY DATA
insert into Driver values ('Pero','Perić','0000','1253'), ('Ana','Anić','1111','54321')
exec AddVehicle 'fha',1,2000,500,1
exec AddVehicle 'keke',5,2012,1800,0
--exec AddTravelOrder '1/2018','2018-10-30',1,1,'2018-10-30','Zagreb','Varaždin',500,600,1,1
exec AddTravelOrder '1/2018','2018-10-30',1,1,'2018-10-30','08:00','09:00','Zagreb','Varaždin',500,600,1,1
exec AddTravelOrder '2/2019','2019-01-23',2,1,'2019-01-23','08:00','10:00','Zagreb','Varaždin',500,600,1,1
exec AddTravelOrder '3/2018','2018-12-30',2,2,'2018-12-30','08:00','20:00','Zagreb','Dubrovnik',600,1200,1,1
--exec AddTravelOrder '2/2018',2,2,'Zagreb','Varaždin',700,800,2,2
--exec AddTravelOrder '3/2018',2,1,'Zagreb','Varaždin',1500,3000,3,3
exec AddVehicle 'a',2,2016,400,0




--select * from Service

--exec EditService 1,'2019-02-20',25,1








