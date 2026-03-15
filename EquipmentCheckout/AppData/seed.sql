PRAGMA foreign_keys = ON;

DROP TABLE IF EXISTS Loans;
DROP TABLE IF EXISTS Holds;
DROP TABLE IF EXISTS Borrowers;
DROP TABLE IF EXISTS EquipmentItems;

CREATE TABLE EquipmentItems (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Category TEXT NOT NULL,
    IsActive INTEGER NOT NULL CHECK (IsActive IN (0,1))
);

CREATE TABLE Borrowers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    StudentNumber TEXT NOT NULL UNIQUE,
    IsActive INTEGER NOT NULL CHECK (IsActive IN (0,1))
);

CREATE TABLE Loans (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EquipmentItemId INTEGER NOT NULL,
    BorrowerId INTEGER NOT NULL,
    LoanDate TEXT NOT NULL,      -- ISO-8601 (YYYY-MM-DD)
    DueDate TEXT NOT NULL,       -- ISO-8601 (YYYY-MM-DD)
    ReturnedDate TEXT NULL,      -- ISO-8601 (YYYY-MM-DD) or NULL
    IsPendingPickup INTEGER DEFAULT 0,
    FOREIGN KEY (EquipmentItemId) REFERENCES EquipmentItems(Id),
    FOREIGN KEY (BorrowerId) REFERENCES Borrowers(Id)
);


Create TABLE Holds (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EquipmentItemId INTEGER NOT NULL,
    BorrowerId INTEGER NOT NULL,
    DatePlaced TEXT NOT NULL,      
    QueuePosition INTEGER NOT NULL,
    FOREIGN KEY (EquipmentItemId) REFERENCES EquipmentItems(Id),
    FOREIGN KEY (BorrowerId) REFERENCES Borrowers(Id)
);


CREATE INDEX IX_Loans_EquipmentItemId ON Loans(EquipmentItemId);
CREATE INDEX IX_Loans_BorrowerId ON Loans(BorrowerId);
CREATE INDEX IX_Loans_ReturnedDate ON Loans(ReturnedDate);

-- EquipmentItems (12)
INSERT INTO EquipmentItems (Name, Category, IsActive) VALUES
('Wet/Dry Vacuum', 'Cleaning', 1),
('Steam Cleaner', 'Cleaning', 1),
('Dual-Action Polisher', 'Polishing', 1),
('Rotary Polisher', 'Polishing', 1),
('Carpet Extractor', 'Interior', 1),
('Air Compressor', 'Tools', 1),
('Microfibre Towel Kit', 'Supplies', 1),
('Detailing Brush Set', 'Supplies', 1),
('Floor Buffer', 'Tools', 0),
('Ozone Generator', 'Interior', 1),
('Pressure Washer', 'Cleaning', 1),
('Extension Cord (50ft)', 'Tools', 1);

-- Borrowers (14)
INSERT INTO Borrowers (Name, StudentNumber, IsActive) VALUES
('Avery Chen', 'S1001001', 1),
('Noah Singh', 'S1001002', 1),
('Mia O''Brien', 'S1001003', 1),
('Liam Tremblay', 'S1001004', 1),
('Emma Roy', 'S1001005', 1),
('Oliver Patel', 'S1001006', 1),
('Sophia Nguyen', 'S1001007', 1),
('Lucas Martin', 'S1001008', 1),
('Charlotte Wilson', 'S1001009', 1),
('Ethan Johnson', 'S1001010', 0),
('Amelia MacDonald', 'S1001011', 1),
('Benjamin Leblanc', 'S1001012', 1),
('Harper Scott', 'S1001013', 1),
('Jackson Lee', 'S1001014', 1);

-- Loans (5 total; 3 active, 2 returned)
-- NOTE: ReturnedDate NULL means "active loan".
INSERT INTO Loans (EquipmentItemId, BorrowerId, LoanDate, DueDate, ReturnedDate) VALUES
(1, 1, '2026-02-10', '2026-02-17', NULL),
(2, 1, '2026-02-12', '2026-02-20', NULL),
(3, 2, '2026-02-15', '2026-02-22', NULL),
(4, 3, '2026-01-20', '2026-01-27', '2026-01-26'),
(5, 4, '2026-01-25', '2026-02-01', '2026-01-31');


INSERT INTO Holds (EquipmentItemId, BorrowerId, DatePlaced, QueuePosition) VALUES
(1, 3, '2026-02-11', 1),
(1, 4, '2026-02-12', 2),
(2, 5, '2026-02-13', 1),
(3, 6, '2026-02-14', 1),
(4, 7, '2026-02-15', 1);