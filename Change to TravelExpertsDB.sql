
USE TravelExperts
------------- 1.Add New Columns ------------------

-- add username and password columns
ALTER TABLE Customers ADD username VARCHAR(50),
	password VARCHAR(255)  --*username & pin can be null
GO


------------- 2.Add Constraints To New Columns -----------------------

--username must be unique, but allow multiple null values
CREATE UNIQUE NONCLUSTERED INDEX idx_username_notnull ON Customers(username)
WHERE username IS NOT NULL
GO  

--username must be more than 5 digits
ALTER TABLE Customers ADD CONSTRAINT username_length CHECK(datalength(username)>=5)  
GO

--password must be more than 5 digits
ALTER TABLE Customers ADD CONSTRAINT password_length CHECK(datalength(password)>=5)  
GO

------------- 3.Add New Record To Test Constraints -----------------

--insert an admin user for test purpose
INSERT INTO Customers(CustFirstName,CustLastName,CustAddress,CustCity,CustProv,CustPostal,CustCountry,CustHomePhone,CustBusPhone,CustEmail,username,password)
VALUES('Admin','Admin','SAIT','Calgary','AB','I1I 1I1','Canada','000-000','000-000','1@2.com','admin','P@ssw0rd')

--You can try to add a record with same username, an error will be thrown


---------- References ---------------
--https://stackoverflow.com/questions/767657/how-do-i-create-a-unique-constraint-that-also-allows-nulls

--https://www.w3schools.com/sql/sql_unique.asp