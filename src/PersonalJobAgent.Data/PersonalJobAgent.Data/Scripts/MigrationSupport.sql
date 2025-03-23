-- Database Migration Script for Personal Job Agent
-- SQL Server script for creating database migrations

-- Create DatabaseMigration table to track migrations
CREATE TABLE DatabaseMigration (
    MigrationID INT PRIMARY KEY IDENTITY(1,1),
    MigrationName NVARCHAR(255) NOT NULL,
    AppliedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ScriptHash NVARCHAR(64) NOT NULL,
    AppliedBy NVARCHAR(128) NOT NULL
);

-- Create stored procedure for applying migrations
CREATE PROCEDURE ApplyMigration
    @MigrationName NVARCHAR(255),
    @ScriptHash NVARCHAR(64),
    @AppliedBy NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if migration already exists
    IF NOT EXISTS (SELECT 1 FROM DatabaseMigration WHERE MigrationName = @MigrationName)
    BEGIN
        -- Insert migration record
        INSERT INTO DatabaseMigration (MigrationName, ScriptHash, AppliedBy)
        VALUES (@MigrationName, @ScriptHash, @AppliedBy);
        
        RETURN 0; -- Success
    END
    ELSE
    BEGIN
        -- Migration already exists
        RETURN 1; -- Already applied
    END
END;
GO

-- Create stored procedure for checking migration status
CREATE PROCEDURE CheckMigrationStatus
    @MigrationName NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if migration exists
    IF EXISTS (SELECT 1 FROM DatabaseMigration WHERE MigrationName = @MigrationName)
    BEGIN
        SELECT 1 AS IsApplied;
    END
    ELSE
    BEGIN
        SELECT 0 AS IsApplied;
    END
END;
GO

-- Create stored procedure for getting all applied migrations
CREATE PROCEDURE GetAppliedMigrations
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT MigrationName, AppliedDate, ScriptHash, AppliedBy
    FROM DatabaseMigration
    ORDER BY AppliedDate;
END;
GO
