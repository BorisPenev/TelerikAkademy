--04 Write a SQL query to find all information about all departments (use "TelerikAcademy" database).
select * from Departments

--05 Write a SQL query to find all department names.
select Name from Departments

--06 Write a SQL query to find the salary of each employee.
select Salary, EmployeeID from Employees

--07 Write a SQL to find the full name of each employee.
select FirstName + ' ' + LastName AS [Full Name] from Employees

--08 Write a SQL query to find the email addresses of each employee (by his first and last name). Consider that the mail domain is telerik.com. Emails should look like “John.Doe@telerik.com". The produced column should be named "Full Email Addresses".
select FirstName + '.' + LastName + '@telerik.com' AS [Full Email Addresses] from Employees

--09 Write a SQL query to find all different employee salaries.
select DISTINCT Salary from Employees

--10 Write a SQL query to find all information about the employees whose job title is “Sales Representative“.
select * from Employees
where JobTitle = 'Sales Representative'

--11 Write a SQL query to find the names of all employees whose first name starts with "SA".
select FirstName, LastName from Employees
where FirstName like 'SA%'

--12 Write a SQL query to find the names of all employees whose last name contains "ei".
select FirstName, LastName from Employees
where LastName like '%ei%'

--13 Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000].
select Salary from Employees
where Salary between 20000 and 30000

--14 Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600.
select FirstName, LastName from Employees
where Salary = 25000 or Salary = 14000 or Salary = 12500 or Salary = 23600

--15 Write a SQL query to find all employees that do not have manager.
select FirstName, LastName from Employees
where ManagerID is null

--16 Write a SQL query to find all employees that have salary more than 50000. Order them in decreasing order by salary.
select FirstName, LastName, Salary from Employees
where Salary > 50000
order by Salary desc

--17 Write a SQL query to find the top 5 best paid employees.
select top 5 FirstName, LastName, Salary from Employees
order by Salary desc

--18 Write a SQL query to find all employees along with their address. Use inner join with ON clause.
select e.FirstName, e.LastName, a.AddressText from Employees e
join Addresses a 
on e.AddressID = a.AddressID

--19 Write a SQL query to find all employees and their address. Use equijoins (conditions in the WHERE clause).
select e.FirstName, e.LastName, a.AddressText 
from Employees e, Addresses a 
where e.AddressID = a.AddressID

--20 Write a SQL query to find all employees along with their manager.
select e.FirstName, e.LastName, m.FirstName + ' ' + m.LastName AS [Manager Name]
from Employees e, Employees m
where e.ManagerID = m.EmployeeID

--21 Write a SQL query to find all employees, along with their manager and their address. Join the 3 tables: Employees e, Employees m and Addresses a.
select e.FirstName, e.LastName, m.FirstName + ' ' + m.LastName AS [Manager Name], a.AddressText
from Employees e
	join Employees m
on e.ManagerID = m.EmployeeID
	join Addresses a
	on e.AddressID = a.AddressID

--22 Write a SQL query to find all departments and all region names, country names and city names as a single list. Use UNION.
SELECT Name FROM Departments
UNION ALL
SELECT Name FROM Towns

--23 Write a SQL query to find all the employees and the manager for each of them along with the employees that do not have manager. User right outer join. Rewrite the query to use left outer join.
SELECT 
	e.FirstName + ' ' + e.LastName as [Employer Name],
	m.FirstName + ' ' + m.LastName as [Manager Name]
FROM 
	Employees as e
	left join Employees m
	on e.ManagerID = m.EmployeeID


SELECT 
	e.FirstName + ' ' + e.LastName as [Employer Name],
	m.FirstName + ' ' + m.LastName as [Manager Name]
FROM 
	Employees as e
	right join Employees m
	on e.ManagerID = m.EmployeeID


--24 Write a SQL query to find the names of all employees from the departments "Sales" and "Finance" whose hire year is between 1995 and 2000
SELECT 
	e.FirstName + ' ' + e.LastName as [Employer Name],
	d.Name,
	e.HireDate
FROM 
	Employees e, Departments d
Where
	e.HireDate BETWEEN '1995-01-01' AND '2000-02-01'
	and d.Name = 'Sales' or d.Name = 'Finance'
