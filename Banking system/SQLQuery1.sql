CREATE TABLE [dbo].[Workers] (
    [Id]         INT            NOT NULL PRIMARY KEY,
    [Name]       NVARCHAR (255) NOT NULL,
    [Surname]    NVARCHAR (255) NOT NULL,
    [Patronymic] NVARCHAR (255) NOT NULL,
    [Post]       NVARCHAR (255) NOT NULL
    --CONSTRAINT [FK_Workers_ToCredits] FOREIGN KEY ([Id]) REFERENCES [Credits]([Id_worker])
);



CREATE TABLE[dbo].[Requests]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Id_credit_type] int NOT NULL,
	[Id_client] int NOT NULL,
	[Result] BIT NOT NULL
    --CONSTRAINT [FK_REQUESTS_TOcREDITS] FOREIGN KEY ([Id]) REFERENCES [Credits]([Id_request])

)



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
    [UserName]     NVARCHAR(255) NOT NULL,
    [Password]      NVARCHAR(255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
    --CONSTRAINT [FK_Clients_ToTable] FOREIGN KEY (Id) REFERENCES [Requests]([Id_client])
);





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


--drop table Clients;
--drop table Credits;
--drop table CreditTypes;
--drop table Requests;
--drop table Workers;






















INSERT INTO [dbo].[CreditTypes] VALUES
(1, 'Ипотека', 10, 60),
(2, 'Потребительский', 11, 36),
(3, 'Авто кредит', 9, 48),
(4, 'Международный', 5, 24)



INSERT INTO Clients VALUES
(1,'Нифакин', 'Егор', 'Сергеевич', 350000, '79817066941', '1'),
(2,'Голиков', 'Максим', 'Максимович', 25000, '754866941', '2'),
(3,'Лоза', 'Серафим', 'Дмитриевич', 50000, '7987865745', '3'),
(4,'Айдаров', 'Никита', 'Викторович', 45000, '7838945784','4'),
(5,'Дубовский', 'Андрей', 'Сергеевич', 17200, '798179056','5'),
(6,'Медоев', 'Вадим', 'Альбертович', 56000, '798567308','6'),
(7,'Кулинкович', 'Михаил', 'Сергеевич', 2200, '779486579','7'),
(8,'Сарычев', 'Максим', 'Андреевич', 72000, '7593845793','8'),
(9,'Красильников', 'Петр', 'Максимович', 50000, '78925306432','9'),
(10,'Непеина', 'Олеся', 'Олеговна', 90000, '795489357','10')


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




INSERT INTO Workers VALUES
(1,'Зорин', 'Егор', 'Альбертович', 'Менеджер'),
(2,'Холодов', 'Кирилл', 'Викторович', 'Менеджер'),
(3,'Егоров', 'Никита', 'Андреевич', 'Менеджер'),
(4,'Щербинин', 'Макс', 'Степанович', 'Менеджер'),
(5,'Абанин', 'Кирилл', 'Дмитриевич', 'Администратор'),
(6,'Шубина', 'Софья', 'Андреевна', 'Менеджер'),
(7,'Яшуткин', 'Алексей', 'Витальевич', 'Менеджер'),
(8,'Рыбин', 'Егор', 'Сергеевич', 'Менеджер'),
(9,'Сергеев', 'Никита', 'Альбертович', 'Менеджер'),
(10,'Кудряшева', 'Юлия', 'Игоревна', 'Менеджер')




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



