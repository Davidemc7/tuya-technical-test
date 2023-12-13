CREATE DATABASE tuya_technical_test
GO

USE tuya_technical_test
GO

CREATE TABLE customer (
    customer_id INT NOT NULL IDENTITY,
	given_name VARCHAR(255) NOT NULL,
	family_name VARCHAR(255) NOT NULL,
    email VARCHAR(255),
    phone VARCHAR(10),
    [address] VARCHAR(1024),
	creator INT NOT NULL,
	date_created DATETIME NOT NULL,
	retired BIT NOT NULL DEFAULT 0,
	retired_by INT,
	date_retired DATETIME,
	PRIMARY KEY (customer_id)
)

CREATE TABLE [order] (
    order_id INT NOT NULL IDENTITY,
    customer_id INT NOT NULL,
    order_date DATETIME NOT NULL,
    total_amount DECIMAL(10, 2) NOT NULL,
    paid BIT NOT NULL DEFAULT 0,
	creator INT NOT NULL,
	date_created DATETIME NOT NULL,
	retired BIT NOT NULL DEFAULT 0,
	retired_by INT,
	date_retired DATETIME,
    PRIMARY KEY (order_id),
    FOREIGN KEY (customer_id) REFERENCES customer(customer_id)
)