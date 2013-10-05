--01
--Create a database with two tables: Persons(Id(PK), FirstName, LastName, SSN)
--and Accounts(Id(PK), PersonId(FK), Balance). Insert few records for testing.
--Write a stored procedure that selects the full names of all persons.


CREATE DATABASE Contact

USE Contact
GO

CREATE TABLE Persons(
  Id int NOT NULL PRIMARY KEY IDENTITY,
  FirstName varchar(20),
  LastName varchar(20),
  SSN varchar(20) NOT NULL )
GO

CREATE TABLE Accounts(
  Id int NOT NULL PRIMARY KEY IDENTITY,
  PersonId int FOREIGN KEY
	REFERENCES Persons(Id),
  Balance money)
GO

--create some people
CREATE PROC usp_NewPerson(
  @firstName nvarchar(20), 
  @lastName nvarchar(20),
  @ssn nvarchar(20))
AS
  INSERT INTO Persons(FirstName, LastName, SSN)
  VALUES (@firstName, @lastName, @ssn)
  RETURN SCOPE_IDENTITY()
GO

CREATE PROC usp_Account(
  @balance money)
AS
  INSERT INTO Accounts(Balance)
  VALUES (@balance)
  RETURN SCOPE_IDENTITY()
GO

EXEC usp_NewPerson 'pesho','goshev','123456789'
EXEC usp_NewPerson 'pesho2','goshev2','123456789'
EXEC usp_NewPerson 'pesho3','goshev3','123456789'
EXEC usp_Account '100.10'
EXEC usp_Account '200.22'
EXEC usp_Account '500.55'

-- end creation of persons

SELECT (FirstName + ' '+ LastName) AS FullName
FROM Persons
GO

--02
--Create a stored procedure that accepts a number as a parameter 
--and returns all persons who have more money in their accounts 
--than the supplied number.

CREATE PROC usp_SelectMoreMoney 
	(@number INT = 200)
AS
	SELECT p.FirstName + ' ' + p.LastName
	FROM Persons p, Accounts a
	WHERE p.ID = a.ID
	AND a.Balance >= @number
GO
 
EXEC usp_SelectMoreMoney 400
GO

--03
--Create a function that accepts as parameters – sum, yearly interest
--rate and number of months. It should calculate and return the new sum. 
--Write a SELECT to test whether the function works as expected.

CREATE FUNCTION ufn_CalculateInterest 
 (@sum float, @InterestRate float, @months float)
 RETURNS float
AS
	BEGIN
		RETURN (@sum * (@InterestRate / 100) * (@months / 12)) + @sum
	END
GO

SELECT dbo.ufn_CalculateInterest (1000, 10, 6)
GO

--04
--Create a stored procedure that uses the function from the previous 
--example to give an interest to a person's account for one month. 
--It should take the AccountId and the interest rate as parameters.

CREATE PROC usp_GiveInterest(@accountID INT, @yearlyInterest NUMERIC(18, 2))
AS
 UPDATE Accounts
 SET Balance = CONVERT (money,dbo.ufn_CalculateInterest(Balance, @yearlyInterest, 1))
 WHERE ID = @accountID
GO

--05
--Add two more stored procedures WithdrawMoney( AccountId, money) and 
--DepositMoney (AccountId, money) that operate in transactions.

CREATE PROC dbo.usp_WithdrawMoney (@accountID INT, @money money)
AS
BEGIN TRAN
	UPDATE Accounts
	SET Balance = Balance - @money
	WHERE ID = @accountID
COMMIT TRAN
GO
 
CREATE PROC dbo.usp_DepositMoney (@accountID INT, @money money)
AS
BEGIN TRAN
	UPDATE Accounts
	SET Balance = Balance + @money
	WHERE ID = @accountID
COMMIT TRAN
GO
 
EXEC dbo.usp_WithdrawMoney 2, 50
EXEC dbo.usp_WithdrawMoney 1, 50
EXEC dbo.usp_DepositMoney 1, 200
GO

--06
--Create another table – Logs(LogID, AccountID, OldSum, NewSum). 
--Add a trigger to the Accounts table that enters a new entry into the Logs
--table every time the sum on an account changes.

CREATE TABLE Logs(
  LogID int NOT NULL PRIMARY KEY IDENTITY,
  AccountID int FOREIGN KEY
	REFERENCES Accounts(Id),
  OldSum money NOT NULL,
  NewSum money NOT NULL)
GO

CREATE TRIGGER tr_AccountsUpdate ON Accounts FOR UPDATE
AS
	INSERT INTO Logs (OldSum, NewSum, AccountID)
	SELECT d.Balance,
           i.Balance,
           d.ID
	FROM deleted d, inserted i
    WHERE d.ID = i.ID
GO

EXEC dbo.usp_DepositMoney 1, 200
GO

--07
--Define a function in the database TelerikAcademy that returns all 
--Employee's names (first or middle or last name) and all town's names
--that are comprised of given set of letters. Example 'oistmiahf' 
--will return 'Sofia', 'Smith', … but not 'Rob' and 'Guy'.

--folowing 12 lines gives access to OLE Functions in SQL Server
sp_configure 'show advanced options', 1  
GO   
RECONFIGURE;  
GO  
sp_configure 'Ole Automation Procedures', 1  
GO   
RECONFIGURE;  
GO   
sp_configure 'show advanced options', 1  
GO   
RECONFIGURE; 
GO

--function for RegEx using VBScript.RegExp
CREATE FUNCTION dbo.regexFind
	(@source varchar(5000),@regexp varchar(1000),@ignoreCase bit = 0)
	RETURNS bit AS
BEGIN
	DECLARE @hr integer
	DECLARE @objRegExp integer
	DECLARE @results bit
	
	SET @results = 0

	EXECUTE @hr = sp_OACreate 'VBScript.RegExp', @objRegExp OUTPUT
	IF @hr <> 0 
	BEGIN
		EXEC @hr = sp_OADestroy @objRegExp
	RETURN NULL
	END
	EXECUTE @hr = sp_OASetProperty @objRegExp, 'Pattern', @regexp
	IF @hr <> 0 
	BEGIN
		EXEC @hr = sp_OADestroy @objRegExp
	RETURN NULL
	END
	EXECUTE @hr = sp_OASetProperty @objRegExp, 'Global', false
	IF @hr <> 0 
	BEGIN
		EXEC @hr = sp_OADestroy @objRegExp
	RETURN NULL
	END
	EXECUTE @hr = sp_OASetProperty @objRegExp, 'IgnoreCase', @ignoreCase
	IF @hr <> 0 
	BEGIN
		EXEC @hr = sp_OADestroy @objRegExp
	RETURN NULL
	END	
	EXECUTE @hr = sp_OAMethod @objRegExp, 'Test', @results OUTPUT, @source
	IF @hr <> 0 
	BEGIN
		EXEC @hr = sp_OADestroy @objRegExp
	RETURN NULL
	END
	EXECUTE @hr = sp_OADestroy @objRegExp
	IF @hr <> 0 
	BEGIN
		RETURN NULL
	END
	
	RETURN @results
END
GO

--returns all employees with given town AND 
--(first name OR last name OR middle name)
CREATE FUNCTION fn_Find 
	( @regularExpression nvarchar(30) )
	RETURNS TABLE
AS
RETURN 
	SELECT e.FirstName, e.MiddleName, e.LastName, t.Name AS Town
	FROM TelerikAcademy.dbo.Employees e, TelerikAcademy.dbo.Addresses a, TelerikAcademy.dbo.Towns t
	WHERE e.AddressID = a.AddressID
	AND a.TownID = t.TownID
	AND 1 = dbo.regexFind(LOWER(t.Name), @regularExpression,1)
	AND (1 = dbo.regexFind(LOWER(e.FirstName), @regularExpression,1)
		OR 1 = dbo.regexFind(LOWER(ISNULL(e.MiddleName, '')), @regularExpression,1)
		OR 1 = dbo.regexFind(LOWER(e.LastName), @regularExpression,1))
GO

--examples
SELECT * FROM fn_Find('^[oistmiahf]+$')
GO

--08
--Using database cursor write a T-SQL script that scans all employees and their
--addresses and prints all pairs of employees that live in the same town.

DECLARE empCursor CURSOR READ_ONLY FOR 
	SELECT e.FirstName, e.LastName, t.Name,
                o.FirstName, o.LastName
        FROM TelerikAcademy.dbo.Employees e
                INNER JOIN Addresses a
                        ON a.AddressID = e.AddressID
                INNER JOIN Towns t
                        ON t.TownID = a.TownID,
        TelerikAcademy.dbo.Employees o
                INNER JOIN Addresses a1
                        ON a1.AddressID = o.AddressID
                INNER JOIN Towns t1
                        ON t1.TownID = a1.AddressID 
	
	OPEN empCursor
        DECLARE @firstName1 NVARCHAR(50)
        DECLARE @lastName1 NVARCHAR(50)
        DECLARE @town NVARCHAR(50)
        DECLARE @firstName2 NVARCHAR(50)
        DECLARE @lastName2 NVARCHAR(50)
        FETCH NEXT FROM empCursor
                INTO @firstName1, @lastName1, @town, @firstName2, @lastName2
 
        WHILE @@FETCH_STATUS = 0
                BEGIN
					PRINT @firstName1 + ' ' + @lastName1 +
					      '     ' + @town + '      ' + @firstName2 + ' ' + @lastName2
					FETCH NEXT 
						FROM empCursor
                        INTO @firstName1, @lastName1, @town, @firstName2, @lastName2
                END
 
        CLOSE empCursor
        DEALLOCATE empCursor	
GO

--09
--* Write a T-SQL script that shows for each town a list of all employees that live in it. 

USE TelerikAcademy
DECLARE empCursor CURSOR READ_ONLY FOR
SELECT Name FROM Towns
OPEN empCursor
DECLARE @townName VARCHAR(50), @userNames VARCHAR(MAX)
FETCH NEXT FROM empCursor INTO @townName 
 
WHILE @@FETCH_STATUS = 0
	BEGIN
		BEGIN
		DECLARE nameCursor CURSOR READ_ONLY FOR
		SELECT a.FirstName, a.LastName
		FROM Employees a
		JOIN Addresses adr
		ON a.AddressID = adr.AddressID
		JOIN Towns t1
		ON adr.TownID = t1.TownID
		WHERE t1.Name = @townName
		OPEN nameCursor
		
		DECLARE @firstName VARCHAR(50), @lastName VARCHAR(50)
		FETCH NEXT FROM nameCursor INTO @firstName,  @lastName
		WHILE @@FETCH_STATUS = 0
		    BEGIN
		            SET @userNames = CONCAT(@userNames, @firstName, ' ', @lastName, ', ')
		            FETCH NEXT FROM nameCursor
		            INTO @firstName,  @lastName
		    END
		    CLOSE nameCursor
		    DEALLOCATE nameCursor
		            END
		            SET @userNames = LEFT(@userNames, LEN(@userNames) - 1)
		PRINT @townName + ' -> ' + @userNames
		FETCH NEXT FROM empCursor
		INTO @townName
	END
CLOSE empCursor
DEALLOCATE empCursor
 
GO

--10
--Define a .NET aggregate function StrConcat that takes as input a sequence 
--of strings and return a single string that consists of the input strings 
--separated by ','.

--source http://www.mssqltips.com/sqlservertip/2022/concat-aggregates-sql-server-clr-function/

--copy file Aggregate.dll to c:\ !

--folowing 3 lines gives access to .NET scripts
sp_configure 'clr enabled', 1  
GO   
RECONFIGURE;  
GO  

IF OBJECT_ID('dbo.concat') IS NOT NULL DROP Aggregate concat 
GO 

IF EXISTS (SELECT * FROM sys.assemblies WHERE name = 'concat_assembly') 
       DROP assembly concat_assembly; 
GO      

CREATE Assembly concat_assembly 
   AUTHORIZATION dbo 
   FROM 'c:\Aggregate.dll' 
   WITH PERMISSION_SET = SAFE; 
GO 

CREATE AGGREGATE dbo.concat 
	( @Value NVARCHAR(MAX), @Delimiter NVARCHAR(4000)) 
	RETURNS NVARCHAR(MAX) 
EXTERNAL Name concat_assembly.concat; 
GO 

SELECT dbo.concat(FirstName + ' ' + LastName,',') as ConcatNames
FROM Employees
 