IF NOT EXISTS 
(
    SELECT 
        1 
    FROM sys.schemas 
    WHERE name = 'Catalogue'
)
BEGIN
    EXEC('CREATE SCHEMA Catalogue');
END
GO