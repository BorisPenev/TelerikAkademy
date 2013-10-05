--01 Write a SQL query to find the names and salaries of the employees that take the minimal salary in the company. Use a nested SELECT statement.
SELECT FirstName, LastName, Salary
FROM Employees
WHERE salary = (SELECT MIN(Salary) FROM Employees)

--02 Write a SQL query to find the names and salaries of the employees that have a salary that is up to 10% higher than the minimal salary for the company.
SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary <= ((SELECT MIN(Salary) FROM Employees) + (SELECT MIN(Salary) FROM Employees)*0.1)

--03 Write a SQL query to find the full name, salary and department of the employees that take the minimal salary in their department. Use a nested SELECT statement.
select d.DepartmentID, e.Salary, e.FirstName, e.LastName
from Departments d, Employees e
where e.Salary = (select min(Salary)
				from Employees
				where Employees.DepartmentID = d.DepartmentID)
order by DepartmentID

--04 Write a SQL query to find the average salary in the department #1.
select AVG(Salary)
from Employees
where DepartmentID = 1

--05 Write a SQL query to find the average salary  in the "Sales" department.
select AVG(e.Salary)
from Employees e, Departments d
where d.Name = 'Sales' and e.DepartmentID = d.DepartmentID

--06 Write a SQL query to find the number of employees in the "Sales" department.
select COUNT(*)
from Employees e, Departments d
where d.Name = 'Sales' and e.DepartmentID = d.DepartmentID

--07 Write a SQL query to find the number of all employees that have manager.
select count(e.ManagerID)
from Employees e

--08 Write a SQL query to find the number of all employees that have no manager.
select count(*)
from Employees e
where e.ManagerID is null

--09 Write a SQL query to find all departments and the average salary for each of them.
select AVG(Salary), DepartmentID
from Employees
group by DepartmentID

--10 Write a SQL query to find the count of all employees in each department and for each town.
select COUNT(*) as [count], e.DepartmentID, t.TownID
from Employees e, Addresses a, Towns t
where e.AddressID = a.AddressID and a.TownID = t.TownID
group by t.TownID, e.DepartmentID
order by t.TownID

--11 Write a SQL query to find all managers that have exactly 5 employees. Display their first name and last name.
select FirstName, EmployeeID
from Employees e
where 5 = (
			select count(*)
			from Employees m
			where m.ManagerID = e.EmployeeID
			)

--12 Write a SQL query to find all employees along with their managers. For employees that do not have manager display the value "(no manager)".
select COALESCE(CONVERT(nvarchar(50), ManagerID), '(no manager)')
from Employees

--13 Write a SQL query to find the names of all employees whose last name is exactly 5 characters long. Use the built-in LEN(str) function.
select FirstName, LastName
from Employees
where LEN(LastName) = 5

--14 Write a SQL query to display the current date and time in the following format "day.month.year hour:minutes:seconds:milliseconds". Search in  Google to find how to format dates in SQL Server.
SELECT convert(varchar, getdate(), 104) + ' ' + convert(varchar, getdate(), 114)

--15 Write a SQL statement to create a table Users. Users should have username, password, full name and last login time. Choose appropriate data types for the table fields. Define a primary key column with a primary key constraint. Define the primary key column as identity to facilitate inserting records. Define unique constraint to avoid repeating usernames. Define a check constraint to ensure the password is at least 5 characters long.
CREATE TABLE dbo.Users
	(
	UserID int NOT NULL IDENTITY (1, 1),
	Username varchar(50) NOT NULL,
	Password nvarchar(50) NOT NULL,
	FullName nvarchar(50) NOT NULL,
	Lastlogin date NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Users ADD CONSTRAINT
	CK_Users CHECK (LEN([Password]) > 4)
GO
ALTER TABLE dbo.Users ADD CONSTRAINT
	PK_Users PRIMARY KEY CLUSTERED 
	(
	UserID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Users ADD CONSTRAINT
	IX_Users UNIQUE NONCLUSTERED 
	(
	UserID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

--16 Write a SQL statement to create a view that displays the users from the Users table that have been in the system today. Test if the view works correctly.
CREATE VIEW [dbo].[vw_Users]
AS
SELECT        UserID, Username, Password, FullName, LastLogin
FROM            dbo.Users
WHERE        (LastLogin = GETDATE())

GO

--17 Write a SQL statement to create a table Groups. Groups should have unique name (use unique constraint). Define primary key and identity column.
CREATE TABLE dbo.Groups
	(
	GroupID int NOT NULL IDENTITY (1, 1),
	Name nvarchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Groups ADD CONSTRAINT
	PK_Groups PRIMARY KEY CLUSTERED 
	(
	GroupID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Groups ADD CONSTRAINT
	IX_Groups UNIQUE NONCLUSTERED 
	(
	GroupID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

--18 Write a SQL statement to add a column GroupID to the table Users. Fill some data in this new column and as well in the Groups table. Write a SQL statement to add a foreign key constraint between tables Users and Groups tables.
ALTER TABLE Users
ADD GtoupId int NOT NULL
 
ALTER TABLE Users
ADD CONSTRAINT fk_Users_Groups
FOREIGN KEY (GroupId)
REFERENCES Groups(Id)
 
--19. Write SQL statements to insert several records in the Users and Groups tables.
INSERT INTO Users (Username, Password, FullName, LastLogin, GroupId)
VALUES ('Brum', '123456', 'Brum Brum', GETDATE(), 1)
 
INSERT INTO Users (Username, Password, FullName, LastLogin, GroupId)
VALUES ('Bzzz', 'qwerty', 'Bzzzzzzzzzzzzzzzz', GETDATE(), 2)
 
INSERT INTO Groups (Name)
VALUES ('C#')
 
INSERT INTO Groups (Name)
VALUES ('JS')

--20 Write SQL statements to update some of the records in the Users and Groups tables.
UPDATE Users SET Password = 'mishkaaaa' WHERE UserID = 1
UPDATE Users SET FullName = 'kuku kuku' WHERE UserID = 2

--21 Write SQL statements to delete some of the records from the Users and Groups tables.
DELETE FROM Users WHERE UserID = 1

--22 Write SQL statements to insert in the Users table the names of all employees from the Employees table. Combine the first and last names as a full name. For username use the first letter of the first name + the last name (in lowercase). Use the same for the password, and NULL for last login time.
INSERT INTO Users (Username, Password, FullName, GroupId)
SELECT LOWER(LEFT(e.FirstName, 1) + '.' + e.LastName),
           LOWER(LEFT(e.FirstName, 1) + '.' + e.LastName + '_pass'),
           e.FirstName + ' ' + e.LastName,
           2
FROM Employees e

--23 Write a SQL statement that changes the password to NULL for all users that have not been in the system since 10.03.2010.
UPDATE Users SET Password = NULL
where Lastlogin < convert(varchar, '10.03.2015' , 104)

--24 Write a SQL statement that deletes all users without passwords (NULL password).
DELETE FROM Users WHERE Password is null

--25 Write a SQL query to display the average employee salary by department and job title.
select AVG(Salary), JobTitle, DepartmentID
from Employees
group by JobTitle, DepartmentID

--26 Write a SQL query to display the minimal employee salary by department and job title along with the name of some of the employees that take it.
select MIN(FirstName), MIN(Salary), JobTitle, DepartmentID
from Employees
group by JobTitle, DepartmentID

--27 Write a SQL query to display the town where maximal number of employees work.
WITH summary AS (
		select t.TownID, count(e.EmployeeID) as [empCount]
		from Employees e, Addresses a, Towns t
		where e.AddressID = a.AddressID and a.TownID = t.TownID
		group by t.TownID)
		select * from summary s where s.empCount = (select MAX(empCount) from summary)

--28 Write a SQL query to display the number of managers from each town.
select t.TownID, count(e.EmployeeID) as [managerCount]
from Employees e, Employees m, Addresses a, Towns t
where e.AddressID = a.AddressID and a.TownID = t.TownID and m.EmployeeID = e.ManagerID
group by t.TownID

--29 Write a SQL to create table WorkHours to store work reports for each employee (employee id, date, task, hours, comments). Don't forget to define  identity, primary key and appropriate foreign key. 
--Issue few SQL statements to insert, update and delete of some data in the table.
--Define a table WorkHoursLogs to track all changes in the WorkHours table with triggers. For each change keep the old record data, the new record data and the command (insert / update / delete).
CREATE TABLE WorkHours (
 EmployeeID INT IDENTITY,
 OnDate DATETIME,
 Task NVARCHAR(50),
 HoursWorked INT,
 Comments nvarchar(50)
 CONSTRAINT PK_EmployeeID PRIMARY KEY (EmployeeID)
 CONSTRAINT FK_EmployeeID FOREIGN KEY (EmployeeID)
  REFERENCES Employees (EmployeeID)
)
GO
 
INSERT INTO WorkHours
SELECT
 GETDATE() AS OnDate,
 'sometask1' AS Task,
 6 AS HoursWorked,
 'no comment' AS Comments
 
UPDATE WorkHours
 SET Task = 'no current task'
 WHERE Task = 'sometask1'


CREATE TABLE WorkHoursLogs
(
 NewEmployeeID int,
 NewOnDate datetime,
 NewTask nvarchar(50),
 NewHoursWorked int,
 NewComments nvarchar(50),
    CONSTRAINT PK_WorkHoursLogs PRIMARY KEY(NewEmployeeID),
)
GO
 
CREATE TRIGGER tr_Update ON dbo.WorkHours FOR UPDATE
AS
 BEGIN
  INSERT INTO dbo.WorkHoursLogs(NewOnDate, NewTask, NewHoursWorked,NewComments)
  SELECT  i.OnDate ,
          i.Task,
          i.HoursWorked,
          i.Comments         
  FROM inserted i
 END
GO

-- for testing purposes
DROP TRIGGER tr_Update
GO
 
CREATE TRIGGER tr_Insert ON dbo.WorkHours FOR INSERT
AS
 BEGIN
  INSERT INTO dbo.WorkHoursLogs(NewOnDate, NewTask, NewHoursWorked,NewComments)
  SELECT  i.OnDate,
          i.Task,
          i.HoursWorked,
          i.Comments
  FROM inserted i
 END
GO

-- for testing purposes
DROP TRIGGER tr_Insert
GO
 
CREATE TRIGGER tr_Delete ON dbo.WorkHours FOR DELETE
AS
 BEGIN
  INSERT INTO dbo.WorkHoursLogs(NewOnDate, NewTask, NewHoursWorked,NewComments)
  SELECT  d.OnDate,
          d.Task,
          d.HoursWorked,
          d.Comments
  FROM deleted d
 END
GO

-- for testing purposes
DROP TRIGGER tr_Delete
GO

--30 Start a database transaction, delete all employees from the 'Sales' department along with all dependent records from the pother tables. At the end rollback the transaction.
BEGIN TRAN
 
ALTER TABLE Departments
DROP CONSTRAINT FK_Departments_Employees
 
DELETE FROM Employees
WHERE Employees.DepartmentID = (
        SELECT DepartmentID
        FROM Departments
        WHERE Departments.Name = 'Sales')
 
ROLLBACK TRAN

--31 Start a database transaction and drop the table EmployeesProjects. Now how you could restore back the lost table data?
BEGIN TRAN
DROP TABLE EmployeesProjects
ROLLBACK

--32 Find how to use temporary tables in SQL Server. Using temporary tables backup all records from EmployeesProjects and restore them back after dropping and re-creating the table.
CREATE TABLE #LocalTempTable(
        EmployeeID INT NOT NULL,
        ProjectID INT NOT NULL,
        CONSTRAINT PK_EmployeeesProjects PRIMARY KEY (EmployeeID, ProjectID),
)
GO

INSERT INTO #LocalTempTable
SELECT * FROM EmployeesProjects
GO
 
DROP TABLE EmployeesProjects
 
CREATE TABLE EmployeesProjects(
 EmployeeID INT NOT NULL,
 ProjectID INT NOT NULL,
 CONSTRAINT PK_EmployeeesProjects PRIMARY KEY (EmployeeID, ProjectID),
 CONSTRAINT FK_EmployeesProjects_Employees FOREIGN KEY (EmployeeID)
         REFERENCES Employees(EmployeeId),
 CONSTRAINT FK_EmployeesProjects_Projects FOREIGN KEY (ProjectID)
         REFERENCES Projects(ProjectId)
)
GO
 
INSERT INTO EmployeesProjects
SELECT * FROM #LocalTempTable