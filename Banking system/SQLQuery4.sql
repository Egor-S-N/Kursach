CREATE TABLE [dbo].[Workers] (
    [Id]         INT            NOT NULL PRIMARY KEY,
    [Name]       NVARCHAR (255) NOT NULL,
    [Surname]    NVARCHAR (255) NOT NULL,
    [Patronymic] NVARCHAR (255) NOT NULL,
    [Post]       NVARCHAR (255) NOT NULL
    --CONSTRAINT [FK_Workers_ToCredits] FOREIGN KEY ([Id]) REFERENCES [Credits]([Id_worker])
);

CREATE TABLE [dbo].[CreditTypes]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] nvarchar(255) NOT NULL,
	[Percent] int NOT NULL,
	[MonthPeriod] int NOT NULL
    --CONSTRAINT [FK_CT_TOREQUESTS] FOREIGN KEY ([Id]) REFERENCES [Requests]([Id_credit_type])

)

CREATE TABLE [dbo].[Clients] (
    [Id]         INT            NOT NULL,
    [Name]       NVARCHAR (255) NOT NULL,
    [Surname]    NVARCHAR (255) NOT NULL,
    [Patronymic] NVARCHAR (255) NOT NULL,
    [Salary]     INT            NOT NULL,
    [Phone]      NVARCHAR (255) NOT NULL,
    [Photo]      NVARCHAR (1000) NOT NULL,
    --[UserName]     NVARCHAR(255) NOT NULL,
    --[Password]      NVARCHAR(255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
    --CONSTRAINT [FK_Clients_ToTable] FOREIGN KEY (Id) REFERENCES [Requests]([Id_client])
);



CREATE TABLE[dbo].[Requests]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Id_credit_type] int NOT NULL,
	[Id_client] int NOT NULL,
	[Result] BIT NOT NULL,
    Foreign key([Id_credit_type]) REFERENCES [CreditTypes](Id),
    Foreign key([Id_client]) REFERENCES Clients([Id])
    --CONSTRAINT [FK_REQUESTS_TOcREDITS] FOREIGN KEY ([Id]) REFERENCES [Credits]([Id_request])

)









CREATE TABLE[dbo].[Credits]
(
[Id] INT NOT NULL PRIMARY KEY,
[Id_worker] int NOT NULL,
[Id_request] int NOT NULL,
[Credit_sum] int NOT NULL,
[Date_of_issue] date NOT NULL,
Foreign key(Id_worker) REFERENCES Workers(Id),
Foreign key(Id_request) REFERENCES Requests(Id)
)


drop table Clients;
drop table Credits;
drop table CreditTypes;
drop table Requests;
drop table Workers;























INSERT INTO [dbo].[CreditTypes] VALUES
(1, N'Ипотека', 10, 60),
(2, N'Потребительский', 11, 36),
(3, N'Авто кредит', 9, 48),
(4, N'Международный', 5, 24)



INSERT INTO Clients VALUES
(1,N'Нифакин', N'Егор', N'Сергеевич', 350000, '79817066941','1'),
(2,N'Голиков', N'Максим', N'Максимович', 25000, '754866941','2'),
(3,N'Лоза', N'Серафим', N'Дмитриевич', 50000, '7987865745','3'),
(4,N'Айдаров', N'Никита', N'Викторович', 45000, '7838945784','3'),
(5,N'Дубовский', N'Андрей', N'Сергеевич', 17200, '798179056','3'),
(6,N'Медоев', N'Вадим', N'Альбертович', 56000, '798567308','3'),
(7,N'Кулинкович', N'Михаил', N'Сергеевич', 2200, '779486579','3'),
(8,N'Сарычев', N'Максим', N'Андреевич', 72000, '7593845793','3'),
(9,N'Красильников', N'Петр', N'Максимович', 50000, '78925306432','3'),
(10,N'Непеина', N'Олеся', N'Олеговна', 90000, '795489357','3')


INSERT INTO Requests VALUES
(1, 1,1, 1),
(2, 2,2, 1),
(3, 1,3, 0),
(4, 1,4, 1),
(5, 3,5, 0),
(6, 4,6, 0),
(7, 2,7, 1),
(8, 1,8, 1),
(9, 1,9, 0),
(10, 4,10, 1)
--delete from Workers
--delete from Clients
--delete from Credits
--delete from Requests
--delete from CreditTypes

INSERT INTO Workers VALUES
(1,N'Зорин', N'Егор', N'Альбертович', N'Менеджер'),
(2,N'Холодов', N'Кирилл', N'Викторович', N'Менеджер'),
(3,N'Егоров', N'Никита', N'Андреевич', N'Менеджер'),
(4,N'Щербинин', N'Макс', N'Степанович', N'Менеджер'),
(5,N'Абанин', N'Кирилл', N'Дмитриевич', N'Администратор'),
(6,N'Шубина', N'Софья', N'Андреевна', N'Менеджер'),
(7,N'Яшуткин', N'Алексей', N'Витальевич', N'Менеджер'),
(8,N'Рыбин', N'Егор', N'Сергеевич', N'Менеджер'),
(9,N'Сергеев', 'Никита', N'Альбертович', N'Менеджер'),
(10,N'Кудряшева', N'Юлия', N'Игоревна', N'Менеджер')



INSERT INTO Credits VALUES
(1,1,1,200000, '2022-10-28'),
(2,2,2,100000, '2022-10-15'),
(3,3,3,300000, '2022-10-16'),
(4,4,4,400000, '2022-10-17'),
(5,4,5,500000, '2022-10-18'),
(6,2,6,600000, '2022-10-19'),
(7,2,7,700000, '2022-10-20'),
(8,1,8,800000, '2022-10-20'),
(9,3,9,2000000, '2022-10-21'),
(10,2,10,2000000, '2022-10-22')



--drop table Clients;
--drop table Credits;
--drop table CreditTypes;
--drop table Requests;
--drop table Workers;



