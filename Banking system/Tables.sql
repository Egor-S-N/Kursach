
INSERT INTO [dbo].[CreditTypes] VALUES
(1, N'Ипотека', 10, 60),
(2, N'Потребительский', 11, 36),
(3, N'Авто кредит', 9, 48),
(4, N'Международный', 5, 24)



INSERT INTO Clients VALUES
(1,N'Нифакин', N'Егор', N'Сергеевич', 350000, N'79817066941', N'1'),
(2,N'Голиков', N'Максим', N'Максимович', 25000, N'754866941', N'2'),
(3,N'Лоза', N'Серафим', N'Дмитриевич', 50000, N'7987865745', N'3'),
(4,N'Айдаров', N'Никита', N'Викторович', 45000, N'7838945784',N'4'),
(5,N'Дубовский', N'Андрей', N'Сергеевич', 17200, N'798179056',N'5'),
(6,N'Медоев', N'Вадим', N'Альбертович', 56000, N'798567308',N'6'),
(7,N'Кулинкович', N'Михаил', N'Сергеевич', 2200, N'779486579',N'7'),
(8,N'Сарычев', N'Максим', N'Андреевич', 72000, N'7593845793',N'8'),
(9,N'Красильников', N'Петр', N'Максимович', 50000, N'78925306432',N'9'),
(10,N'Непеина',N'Олеся', N'Олеговна', 90000, N'795489357',N'10')


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
(1,N'Зорин', N'Егор', N'Альбертович', N'Менеджер'),
(2,N'Холодов', N'Кирилл', N'Викторович', N'Менеджер'),
(3,N'Егоров', N'Никита', N'Андреевич', N'Менеджер'),
(4,N'Щербинин', N'Макс', N'Степанович', N'Менеджер'),
(5,N'Абанин', N'Кирилл', N'Дмитриевич', N'Администратор'),
(6,N'Шубина', N'Софья', N'Андреевна', N'Менеджер'),
(7,N'Яшуткин', N'Алексей', N'Витальевич', N'Менеджер'),
(8,N'Рыбин', N'Егор', N'Сергеевич', N'Менеджер'),
(9,N'Сергеев', N'Никита', N'Альбертович', N'Менеджер'),
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