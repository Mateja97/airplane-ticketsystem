Use airplane_tickets;         
Create table Flights         
(    
   flightId int primary key not null auto_increment,    
   startDestination varchar(20) not null,
   endDestination varchar(20) not null,
   date datetime not null,
   numTransfers int not null,
   numSeats int not null
);
insert into  Flights (startDestination,endDestination,date,numTransfers,numSeats) values ('Beograd','Nis','21.3.2021','0','30');
insert into  Flights (startDestination,endDestination,date,numTransfers,numSeats) values ('Pristina','Kraljevo','25.4.2021','1','3');
insert into  Flights (startDestination,endDestination,date,numTransfers,numSeats) values ('Kraljevo','Beograd','1.3.2021','0','30');
