create trigger SetVipClient
on NTCJournal
after insert
as 
begin
	declare @id int
	select @id = id_NTCclient from inserted
	if (select count(*) from NTCJournal where id_NTCclient = @id) >= 5
	begin
update NTCClients set name_NTCclient = concat(name_NTCclient,' (Постійний клієнт)') where NTCClients.id_NTCclient = @id
	end
end
---------------------------------------------------------------------------------------------------------------

create trigger SetCurrentRegistrationDate
on NTCJournal
after insert
as
declare @currDate date = convert(date, getdate())
declare @currTime time = convert(time(0), getDate())
 if @@ROWCOUNT >= 1
 begin 
  if (select date_NTCjournal  from inserted) is null
 	  begin
  	  update NTCJournal set date_NTCjournal  = @currDate where id_NTCjournal = (select id_NTCjournal from inserted)	
 	  end 
  	 if (select time_NTCjournal from inserted) is null
  	 begin
	update NTCJournal set time_NTCjournal = @currTime where id_NTCjournal = (select id_NTCjournal from inserted)
  	 end
 end;
---------------------------------------------------------------------------------------------------------------

create trigger ChangeRegistrationDate--перенесення
on NTCJournal
after insert
as 
begin
	declare @id int, @date date, @time time
	select @id = id_NTCservice, @date = date_NTCjournal, @time = time_NTCjournal from inserted
	if (select count(*) from NTCJournal where date_NTCjournal = @date and time_NTCjournal = 
	@time and id_NTCservice = @id) > 1
	begin
		update NTCJournal set date_NTCjournal = dateadd(day, 1, @date) 
		where NTCJournal.id_NTCjournal = (select id_NTCjournal from inserted)
	end
end
---------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------

create procedure General_profit
as
begin
select sum(NTCJournal.total_price) as General_profit from NTCJournal
end
exec General_profit
---------------------------------------------------------------------------------------------------------------

create procedure Profit_per_month
as
begin
select sum(NTCJournal.total_price) as Profit_per_month from NTCJournal
--inner join NTCJournal on NTCequipmentInStock.id_type_NTCequipment = NTCJournal.id_NTCequipment
where date_NTCjournal > dateadd(month,-1,getdate())
end
exec Profit_per_month
---------------------------------------------------------------------------------------------------------------

create procedure General_amount_sales
as
begin
select count(NTCJournal.id_NTCjournal) as General_amount_sales from NTCJournal
end
exec General_amount_sales
---------------------------------------------------------------------------------------------------------------

create procedure General_pay_workers_salary
as
begin
select sum(NTCWorkers.salary) as General_pay_workers_salary from NTCWorkers
end
exec General_pay_workers_salary

create procedure General_pay_workers_percentage
as
begin
select sum(NTCJournal.total_price * NTCJournal.NTCworker_percentage * 0.1) as General_pay_workers_percentage from NTCJournal
end
exec General_pay_workers_percentage
---------------------------------------------------------------------------------------------------------------

create procedure General_amount_workers
as
begin
select count(NTCWorkers.id_NTCworker) as General_amount_workers from NTCWorkers
end
exec General_amount_workers
---------------------------------------------------------------------------------------------------------------

create procedure General_amount_clients
as
begin
select count(NTCClients.id_NTCclient) as General_amount_clients from NTCClients
end
exec General_amount_clients
---------------------------------------------------------------------------------------------------------------

create procedure General_amount_equipment
as
begin
select sum(NTCequipmentInStock.amount_NTCequipment) as General_amount_equipment from NTCequipmentInStock
end
exec General_amount_equipment
---------------------------------------------------------------------------------------------------------------

create procedure General_amount_equipment_providers
as
begin
select sum(NTCequipmentProviders.id_NTCprovider) as General_amount_equipment_providers from NTCequipmentProviders
end
exec General_amount_equipment_providers
---------------------------------------------------------------------------------------------------------------