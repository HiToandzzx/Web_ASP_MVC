CREATE DATABASE DurianExport;

USE DurianExport;

-- CÁC NƯỚC XUẤT KHẨU ĐẾN
CREATE TABLE Countries (
    CountryID INT PRIMARY KEY IDENTITY(1,1),
    CountryName VARCHAR(255) NOT NULL,
    Region VARCHAR(255),
    Population INT,
    Currency VARCHAR(50)
);

-- NHA XUẤT KHẨU
CREATE TABLE Exporters (
    ExporterID INT PRIMARY KEY IDENTITY(1,1),
    ExporterName VARCHAR(255) NOT NULL,
    ContactName VARCHAR(255),
    ContactEmail VARCHAR(255),
    Phone VARCHAR(20)
);

-- QUẢN LÍ XUẤT KHẨU
CREATE TABLE ExportRecords (
    ExportRecordID INT PRIMARY KEY IDENTITY(1,1),
    ExporterID INT,
    CountryID INT,
    ExportDate_Start DATETIME,
	ExportDate_End DATETIME,
    Quantity INT,
    Value DECIMAL(10, 2),
    CONSTRAINT FK_Exporter FOREIGN KEY (ExporterID) REFERENCES Exporters(ExporterID),
    CONSTRAINT FK_Country FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);

-- ĐƠN VỊ VẬN CHUYỂN
CREATE TABLE ExportDeliver (
    DeliverID INT PRIMARY KEY IDENTITY(1,1),
    ExportRecordID INT,
    DeliveryDate DATE,
    DeliveryStatus VARCHAR(50),
    CONSTRAINT FK_ExportRecord FOREIGN KEY (ExportRecordID) REFERENCES ExportRecords(ExportRecordID)
);
