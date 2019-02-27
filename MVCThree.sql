--MVCTHree

Insert dbo.AspNetRoles values ('1', 'Admin')
Insert dbo.AspNetUserRoles values ('74af23df-ab28-495c-85b1-d6d58be77a1b', '1')


select * from AspNetUsers

delete from AspNetUserRoles

UPDATE AspNetUsers SET AspNetUsers.UserName = 'hisoka@gmail.com' WHERE AspNetUsers.Id='74af23df-ab28-495c-85b1-d6d58be77a1b';


create table Supplier(
SupID int not null identity(1,1) primary key,
SupName varchar (30) not null,
SupAddress varchar (40) not null,
SupPhone varchar (14) not null,
MedicineID int
)

ALTER TABLE Supplier
ADD FOREIGN KEY (MedicineID) REFERENCES Instock_Medicine(MedID) on delete cascade;
--alter table Supplier drop constraint FK__Supplier__Medici__5070F446
alter table Supplier drop constraint FK__Supplier__Medici__5441852A

select * from sys.foreign_keys

select * from Instock_Medicine
select * from AspNetUsers

delete from AspNetUsers where Id='76ebadde-f9a5-4c82-bbba-520f0c259a25'

delete from AspNetUsers where Id values "5fd00fa3-0a90-45d7-b00e-db980364081e"
create table Instock_Medicine(
MedID int not null primary key,
MedName varchar (35) not null,
Quantity int not null,
Price money,
SupplierID int
)

ALTER TABLE Instock_Medicine 
DROP COLUMN MedID

ALTER TABLE Instock_Medicine ADD MedID INT IDENTITY(1,1)

ALTER TABLE Instock_Medicine
ADD  PRIMARY KEY  (MedID) 

alter table Instock_Medicine drop constraint PK__Instock___EB77FC361FB96F5A 

create table Wishlist(
WishNum int not null primary key,
WishMedName varchar (25) not null,
RequestAmount int,
Comment varchar (75),
RequesterID int
)

alter table WishList drop constraint PK__Wishlist__E6E9964E532AB1F7 
ALTER TABLE WishList 
DROP COLUMN WishNum

select * from Wishlist
ALTER TABLE WishList ADD WishID INT IDENTITY(1,1)
ALTER TABLE WishList
ADD  PRIMARY KEY  (WishID)

ALTER TABLE WishList ALTER COLUMN RequesterID varchar(40)

alter table Instock_Medicine  add Description varchar(100)
alter table Instock_Medicine add Med_Img varbinary(MaX)
alter table Instock_Medicine add Img_Path nvarchar(100)

alter table Instock_Medicine add Instock_Date Date

update Instock_Medicine set Instock_Date = GETDATE() where  = (select MAX(RecordID) from Record)

select * from Wishlist

create table Record(
RecordID int not null identity(1,1)primary key,
PatientID int,
Date datetime,
Bought_Medicine varchar(20),
Buying_Reason varchar (50),
Comment varchar (50)
)
ALTER TABLE Record ALTER COLUMN PatientID varchar(40)
alter table Record add EditDate datetime 

select * from Record

SELECT TOP 1 * FROM Record ORDER BY PatientID DESC
select * from Instock_Medicine
select MAX(RecordID) from Record
select * from Supplier