
DROP SEQUENCE IF EXISTS restaurantchain.menuitems_id_seq;

DROP SEQUENCE IF EXISTS restaurantchain.customers_id_seq;

DROP SEQUENCE IF EXISTS restaurantchain.orders_id_seq;

DROP SEQUENCE IF EXISTS restaurantchain.orderitems_id_seq;


CREATE SEQUENCE restaurantchain.menuitems_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE restaurantchain.customers_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE restaurantchain.orders_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE restaurantchain.orderitems_id_seq INCREMENT 1 START 1;



DROP TABLE IF EXISTS restaurantchain.MenuItems CASCADE;

CREATE TABLE restaurantchain.MenuItems
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('restaurantchain.menuitems_id_seq"'::text)::regclass),	
	Name varchar(150) UNIQUE NOT NULL,
	Description varchar(1500) NULL,
	Price NUMERIC(3,2),
	IsMeal boolean NOT NULL
);

ALTER TABLE restaurantchain.MenuItems ADD CONSTRAINT PK_MenuItems
	PRIMARY KEY (Id);

	
DROP TABLE IF EXISTS restaurantchain.MealsDishes CASCADE;

CREATE TABLE restaurantchain.MealsDishes
(
	MealId integer,
	DishId integer
);

ALTER TABLE restaurantchain.MealsDishes ADD CONSTRAINT FK_MealsDishes_Dishes
	FOREIGN KEY (DishId) REFERENCES restaurantchain.MenuItems (Id) ON DELETE No Action ON UPDATE No Action;
ALTER TABLE restaurantchain.MealsDishes ADD CONSTRAINT FK_MealsDishes_Meals
	FOREIGN KEY (MealId) REFERENCES restaurantchain.MenuItems (Id) ON DELETE No Action ON UPDATE No Action;
	



DROP TABLE IF EXISTS restaurantchain.Customers CASCADE;

CREATE TABLE restaurantchain.Customers
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('restaurantchain."customers_id_seq"'::text)::regclass),
	Username varchar(500) UNIQUE NOT NULL,
	Password BYTEA NOT NULL,
	FirstName varchar(500) UNIQUE NOT NULL,
	LastName varchar(500) UNIQUE NOT NULL
);
ALTER TABLE restaurantchain.Customers ADD CONSTRAINT PK_Customers
	PRIMARY KEY (Id);

DROP TABLE IF EXISTS restaurantchain.Orders CASCADE;

CREATE TABLE restaurantchain.Orders
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('restaurantchain."orders_id_seq"'::text)::regclass),
	Total NUMERIC(8,2) NOT NULL,
	IsProcessed boolean NOT NULL
);
ALTER TABLE restaurantchain.Orders ADD CONSTRAINT PK_Orders
	PRIMARY KEY (Id);

DROP TABLE IF EXISTS restaurantchain.OrderItems CASCADE;

CREATE TABLE restaurantchain.OrderItems
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('restaurantchain.orderitems_id_seq"'::text)::regclass),
	MenuItemid integer NOT NULL,
	Amount integer NOT NULL,
	Value NUMERIC(8,2) NOT NULL
);
ALTER TABLE restaurantchain.orderitems ADD CONSTRAINT PK_Orderitem
	PRIMARY KEY (Id);
ALTER TABLE restaurantchain.OrderItems ADD CONSTRAINT FK_OrderItems_MenuItems
	FOREIGN KEY (MenuItemid) REFERENCES restaurantchain.MenuItems (Id) ON DELETE No Action ON UPDATE No Action;