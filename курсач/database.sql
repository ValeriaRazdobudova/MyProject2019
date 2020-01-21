create database ntcservice
---------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------
create table TypesNTCequipment
(id_type_NTCequipment int IDENTITY(1,1) not null primary key,
name_type_NTCequipment varchar(50))

create table NTCequipmentProviders
(id_NTCprovider int IDENTITY(1,1) not null primary key,
name_NTCprovider varchar(50),
phone_number_NTCprovider varchar(30),
website_NTCprovider varchar(50),
email_NTCprovider varchar(50),
address_NTCprovider varchar(50))

create table NTCequipmentInStock
(id_NTCequipment int IDENTITY(1,1) not null primary key,
name_NTCequipment varchar(50),
id_type_NTCequipment int not null foreign key (id_type_NTCequipment) references
dbo.TypesNTCequipment(id_type_NTCequipment) on delete cascade on update no action,
id_NTCprovider int not null foreign key (id_NTCprovider) references
dbo.NTCequipmentProviders(id_NTCprovider) on delete cascade on update no action,
amount_NTCequipment int)
---------------------------------------------------------------------------------------------------------------
create table TypesNTCServices
(id_type_NTCservice int IDENTITY(1,1) not null primary key,
name_type_NTCservice varchar(50))

create table NTCServices
(id_NTCservice int IDENTITY(1,1) not null primary key,
name_NTCservice varchar(50),
price_NTCservice int,
id_NTCequipment int not null foreign key (id_NTCequipment) references
dbo.NTCequipmentInStock(id_NTCequipment) on delete cascade on update no action,
id_type_NTCservice int not null foreign key (id_type_NTCservice) references
dbo.TypesNTCServices(id_type_NTCservice) on delete cascade on update no action)
---------------------------------------------------------------------------------------------------------------
create table NTCClients
(id_NTCclient int IDENTITY(1,1) not null primary key,
name_NTCclient varchar(50),
phone_number_NTCclient varchar(30),
email_NTCclient varchar(50),
skype_NTCclient varchar(50))
---------------------------------------------------------------------------------------------------------------
create table NTCWorkers
(id_NTCworker int IDENTITY(1,1) not null primary key,
name_NTCworker varchar(50),
phone_number_NTCworker varchar(30),
email_NTCworker varchar(50),
skype_NTCworker varchar(50),
experience int,
salary int)
---------------------------------------------------------------------------------------------------------------
create table NTCJournal
(id_NTCjournal int IDENTITY(1,1) not null primary key,
id_NTCclient int not null foreign key (id_NTCclient) references
dbo.NTCClients(id_NTCclient) on delete cascade on update no action,
id_NTCworker int not null foreign key (id_NTCworker) references
dbo.NTCWorkers(id_NTCworker) on delete cascade on update no action,
date_NTCjournal date,
time_NTCjournal time,
id_NTCservice int not null foreign key (id_NTCservice) references
dbo.NTCServices(id_NTCservice) on delete cascade on update no action,
amount_NTCequipment int,
total_price int,
NTCworker_percentage int)
---------------------------------------------------------------------------------------------------------------
create table Application
(id_application int IDENTITY(1,1) not null primary key,
phone_number_NTCworker varchar(30),
email_NTCworker varchar(50))
