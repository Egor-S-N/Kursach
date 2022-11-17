create table Accounts(
id int not null primary key,
username nvarchar(255) not null,
pasword nvarchar(255) not null,
accout_type nvarchar(255) not null
)
--drop table Accounts

insert into Accounts values
(1, 'Egor', '111', 'Client')

select * from Accounts