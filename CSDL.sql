create database PHSach
go

use PHSach
go

-- 1. Bảng Năm làm việc
CREATE TABLE WorkYear (
    WorkYearId VARCHAR(50) PRIMARY KEY,
    Year INT NOT NULL,
    Description NVARCHAR(200),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- 2. Bảng Đơn vị
CREATE TABLE Unit (
    UnitId VARCHAR(50) PRIMARY KEY,
    UnitCode NVARCHAR(50) UNIQUE,
    UnitName NVARCHAR(200) NOT NULL,
    Address NVARCHAR(300),
    ContactPerson NVARCHAR(100),
    Phone NVARCHAR(20),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- 3. Bảng Kinh phí đơn vị theo năm
CREATE TABLE UnitBudget (
    UnitBudgetId VARCHAR(50) PRIMARY KEY,
    UnitId VARCHAR(50) NOT NULL,
    WorkYearId VARCHAR(50) NOT NULL,
    InitialBudget DECIMAL(18,2) NOT NULL,
    RemainingBudget DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_UnitBudget_Unit FOREIGN KEY (UnitId) REFERENCES Unit(UnitId),
    CONSTRAINT FK_UnitBudget_WorkYear FOREIGN KEY (WorkYearId) REFERENCES WorkYear(WorkYearId),
    CONSTRAINT UQ_UnitBudget UNIQUE (UnitId, WorkYearId)
);

-- 4. Bảng Đợt mua sách
CREATE TABLE Batch (
    BatchId VARCHAR(50) PRIMARY KEY,
    WorkYearId VARCHAR(50) NOT NULL,
    BatchName NVARCHAR(200) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Batch_WorkYear FOREIGN KEY (WorkYearId) REFERENCES WorkYear(WorkYearId)
);

-- 5. Bảng Sách trong đợt mua
CREATE TABLE BookBatch (
    BookBatchId VARCHAR(50) PRIMARY KEY,          -- ID duy nhất cho sách trong đợt
    BatchId VARCHAR(50) NOT NULL,                 -- FK tới Batch
    BookCode NVARCHAR(50),                        -- Mã số sách (có thể do hệ thống sinh hoặc nhập theo NXB)
    Title NVARCHAR(300) NOT NULL,                 -- Tên sách
    UnitOfMeasure NVARCHAR(50) DEFAULT N'cuốn',   -- Đơn vị tính (cuốn, bộ, tập...)
    Price DECIMAL(18,2) NOT NULL,                 -- Giá 1 đơn vị
    Quantity INT NOT NULL,                        -- Số lượng nhập trong đợt
    CONSTRAINT FK_BookBatch_Batch FOREIGN KEY (BatchId) REFERENCES Batch(BatchId)
);
-- 6. Bảng Phân bổ sách cho đơn vị
CREATE TABLE Allocation (
    AllocationId VARCHAR(50) PRIMARY KEY,
    BatchId VARCHAR(50) NOT NULL,
    BookBatchId VARCHAR(50) NOT NULL,
    UnitBudgetId VARCHAR(50) NOT NULL,
    AllocatedQuantity INT NOT NULL,
    AllocatedCost DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Allocation_Batch FOREIGN KEY (BatchId) REFERENCES Batch(BatchId),
    CONSTRAINT FK_Allocation_BookBatch FOREIGN KEY (BookBatchId) REFERENCES BookBatch(BookBatchId),
    CONSTRAINT FK_Allocation_UnitBudget FOREIGN KEY (UnitBudgetId) REFERENCES UnitBudget(UnitBudgetId)
);

-- 7. Bảng User (tài khoản đăng nhập)
CREATE TABLE [User] (
    UserId VARCHAR(50) PRIMARY KEY,
    Username NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(200) NOT NULL,
    FullName NVARCHAR(200),
    Role NVARCHAR(50), -- admin, manager, viewer
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);
