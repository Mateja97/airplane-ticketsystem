Use airplane_tickets;         
Create table Users         
(    
   Username varchar(50) primary key not null,    
   Password varchar(100) not null,
   Type varchar(10) not null
);
insert into  Users values ('agent1','agent123','Agent');
insert into  Users values ('user1','user123','User');
insert into  Users values ('admin1','admin123','Admin');
