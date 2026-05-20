IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'skin')
BEGIN
    EXEC('CREATE SCHEMA skin');
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'auth')
BEGIN
    EXEC('CREATE SCHEMA auth');
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'catalogue')
BEGIN
    EXEC('CREATE SCHEMA catalogue');
END
GO