IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'skin')
BEGIN
    EXEC('CREATE SCHEMA skin');
END
GO