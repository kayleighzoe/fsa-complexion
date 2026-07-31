IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'catalogue')
BEGIN
    EXEC('CREATE SCHEMA catalogue');
END
GO