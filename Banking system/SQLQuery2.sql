--create table Accounts(
--id int not null primary key,
--username nvarchar(255) not null,
--pasword nvarchar(255) not null,
--accout_type nvarchar(255) not null
--)


--insert into Accounts values
--(1, N'Egor', '111', 'Client')


--create procedure InsertUser
--@Id int,
--@Username NVARCHAR(255),
--@Password NVARCHAR(255),
--@Account_type NVARCHAR(255)

--as 
--BEGIN 
--       SET NOCOUNT ON;
--       IF EXISTS(SELECT username FROM Accounts where username = @Username)
--       BEGIN
--              SELECT -1 AS UserId -- Username exists.
--       END
      

--       ELSE
--       BEGIN
--              INSERT INTO Accounts
--              VALUES
--              (@Id,@Username,@Password,@Account_type)
--             select 1
--              --SELECT SCOPE_IDENTITY() AS UserId -- UserId                     
--     END
--END


CREATE PROCEDURE LoginByUsernamePassword   
@Username varchar(50),   
@Password varchar(50)   
AS   
BEGIN   
SELECT Accounts.username, Accounts.pasword
FROM  Accounts  
WHERE Accounts.username = @Username   
AND   Accounts.pasword = @password   
END
