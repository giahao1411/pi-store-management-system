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
	TotalPrice FLOAT
);

-- Create OrderItem table
CREATE TABLE OrderItem (
	ID VARCHAR(5) PRIMARY KEY,
	OrderID VARCHAR(5),
	ProductID VARCHAR(5),
	Quantity INT
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

ALTER TABLE Orders ALTER COLUMN OrderDate DATETIME

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

INSERT INTO Product  VALUES
	('PD001', 'Wireless Bluetooth Headphones', 'Experience the freedom of wireless sound with these Bluetooth headphones. Featuring noise-cancellation technology and a comfortable over-ear design, they provide crystal-clear audio for music and calls. With up to 30 hours of battery life, you can enjoy uninterrupted listening, whether you''re at home or on the go.', 79.99, 50),
	('PD002', 'Smart LED Desk Lamp', 'Brighten your workspace with this smart LED desk lamp. Equipped with touch controls and adjustable brightness levels, it allows you to create the perfect ambiance for any task. The lamp also features a USB charging port for your devices, making it a stylish and functional addition to your office or study area.', 29.99, 100),
	('PD003', 'Stainless Steel Water Bottle', 'Stay hydrated in style with this double-wall insulated stainless steel water bottle. It keeps drinks cold for up to 24 hours and hot for up to 12 hours. The sleek design fits in most cup holders, and the wide mouth opening makes it easy to fill and clean. Available in multiple colors, it''s perfect for workouts, travel, or everyday use.', 19.99, 75),
	('PD004', 'Portable Bluetooth Speaker', 'Enjoy your favorite tunes anywhere with this portable Bluetooth speaker. With a robust bass and clear treble, it delivers high-quality sound in a compact design. Its waterproof construction makes it ideal for beach days or pool parties. Plus, with a battery life of up to 10 hours, you can take the party with you wherever you go.', 49.99, 40),
	('PD005', 'Ergonomic Office Chair', 'Enhance your productivity with this ergonomic office chair designed for comfort during long hours of work. Featuring adjustable height, lumbar support, and breathable mesh material, it promotes healthy posture and reduces fatigue. The modern design fits seamlessly into any office decor, making it an essential addition to your workspace.', 149.99, 25);

INSERT INTO Orders VALUES
	('OD001', 'CL001', 'EM001', GETDATE(), 129.97)

INSERT INTO OrderItem VALUES
	('OT001', 'OD001', 'PD001', 1),
	('OT002', 'OD001', 'PD002', 1),
	('OT003', 'OD001', 'PD003', 1)

UPDATE Product SET Quantity = Quantity - 1 WHERE ID = 'PD001'
UPDATE Product SET Quantity = Quantity - 1 WHERE ID = 'PD002'
UPDATE Product SET Quantity = Quantity - 1 WHERE ID = 'PD003'

SELECT * FROM OrderItem WHERE OrderID = 'OD001'

SELECT * FROM Product INNER JOIN OrderItem ON Product.ID = OrderItem.ProductID 