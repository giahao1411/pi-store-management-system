USE MASTER 
GO
CREATE DATABASE PiStoreManagementDataBase
GO
USE PiStoreManagementDataBase
GO

SET DATEFORMAT dmy;

-- Create Employee table
CREATE TABLE Employee (
	ID VARCHAR(5) PRIMARY KEY,
	Name NVARCHAR(50),
	Email VARCHAR(20),
	Phone VARCHAR(13),
	Address NVARCHAR(100),
	Salary FLOAT,
	HireDate DATE
);

-- Create Account table
CREATE TABLE Account (
	ID VARCHAR(5),
	Username VARCHAR(50) PRIMARY KEY,
	Password VARCHAR(50), 
	Lock BIT NOT NULL
);

-- Create Client table
CREATE TABLE Client (
	ID VARCHAR(5) PRIMARY KEY,
	Name NVARCHAR(50),
	Email VARCHAR(20),
	Phone VARCHAR(13),
	Address NVARCHAR(100)
);

-- Create Product table
CREATE TABLE Product (
	ID VARCHAR(5) PRIMARY KEY,
	Name NVARCHAR(100),
	Description NVARCHAR(1000),
	Price FLOAT,
	Quantity INT
);

-- Create Orders table
CREATE TABLE Orders (
	ID VARCHAR(5) PRIMARY KEY,
	ClientID VARCHAR(5),
	EmployeeID VARCHAR(5),
	OrderDate DATE,
	TotalPrice float
);

-- Create OrderItem table
CREATE TABLE OrderItem (
	ID VARCHAR(5) PRIMARY KEY,
	OrderID VARCHAR(5),
	ProductID VARCHAR(5),
	Quantity int
);

-- Create Bill table
CREATE TABLE Bill (
	ID VARCHAR(5) PRIMARY KEY,
	OrderID VARCHAR(5),
	ClientID VARCHAR(5),
	EmployeeID VARCHAR(5),
	BillDate DATE,
	TotalPrice FLOAT
);

-- Orders table foreign keys
ALTER TABLE Orders ADD CONSTRAINT fk_orders_client FOREIGN KEY (ClientID) REFERENCES Client(ID);
ALTER TABLE Orders ADD CONSTRAINT fk_orders_employee FOREIGN KEY (EmployeeID) REFERENCES Employee(ID);

-- OrderItem table foreign keys
ALTER TABLE OrderItem ADD CONSTRAINT fk_orderitem_order FOREIGN KEY (OrderID) REFERENCES Orders(ID);
ALTER TABLE OrderItem ADD CONSTRAINT fk_orderitem_product FOREIGN KEY (ProductID) REFERENCES Product(ID);

-- Bill table foreign keys
ALTER TABLE Bill ADD CONSTRAINT fk_bill_order FOREIGN KEY (OrderID) REFERENCES Orders(ID);
ALTER TABLE Bill ADD CONSTRAINT fk_bill_client FOREIGN KEY (ClientID) REFERENCES Client(ID);
ALTER TABLE Bill ADD CONSTRAINT fk_bill_employee FOREIGN KEY (EmployeeID) REFERENCES Employee(ID);

-- Account table forgein key
ALTER TABLE Account ADD CONSTRAINT fk_account_employee FOREIGN KEY (ID) REFERENCES Employee(ID);

-- Add unique for Email and Phone for Employee and Client
ALTER TABLE Employee ADD CONSTRAINT uc_Employee UNIQUE (Email, Phone);
ALTER TABLE Client ADD CONSTRAINT uc_Client UNIQUE (Email, Phone);

ALTER TABLE Employee ALTER COLUMN Email VARCHAR(100)
ALTER TABLE Client ALTER COLUMN Email VARCHAR(100)

SELECT * FROM Employee
SELECT * FROM Account
SELECT * FROM Client
SELECT * FROM Product
SELECT * FROM Orders
SELECT * FROM OrderItem
SELECT * FROM Bill

INSERT INTO Employee VALUES
	('EM001', N'Trần Gia Hào', 'trangiahao498@gmail.com', '0886684075', N'213/21 Lê Quang Sung, P6, Q6, Thành phố Hồ Chí Minh' ,100, GETDATE());

INSERT INTO Account VALUES
	('EM001', 'admin', 'admin123', 0);

INSERT INTO Client VALUES
	('CL001', N'An Kung', 'AnKung@armyspy.com', '0233639601', N'3612 Red Maple Drive, City Of Commerce, CA 90040'),
	('CL002', N'Benjamin T. Gray', 'BenjaminTGray@teleworm.us', '02037312403', N'3961 Asylum AvenueDanbury, CT 06810')