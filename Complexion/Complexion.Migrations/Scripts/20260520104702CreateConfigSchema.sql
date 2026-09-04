IF NOT EXISTS 
(
    SELECT 
        1 
    FROM sys.schemas 
    WHERE name = 'Config'
)
BEGIN
    EXEC('CREATE SCHEMA config');
END
GO