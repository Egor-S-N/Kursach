create table Accounts(
id int not null primary key,
username nvarchar(255) not null,
pasword nvarchar(255) not null,
accout_type nvarchar(255) not null
)
--drop table Accounts

insert into Accounts values
(2, 'Egor', '111', 'Client')