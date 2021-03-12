Use airplane_tickets;         
Create table Reservations         
(    
   reservationID int primary key not null auto_increment,    
   username varchar(100) not null,
   flightID int not null,
   accepted boolean,
   agent varchar(100) not null,
   numSeats int not null
);
insert into  Reservations (username,flightID,accepted,agent,numSeats) values  ('user1',1,false,'',1);
insert into  Reservations (username,flightID,accepted,agent,numSeats) values  ('user1',2,false,'',2);
insert into  Reservations (username,flightID,accepted,agent,numSeats) values  ('user1',3,false,'',3);
