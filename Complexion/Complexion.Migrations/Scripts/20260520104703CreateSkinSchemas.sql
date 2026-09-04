IF NOT EXISTS 
(
    SELECT 
        1 
    FROM sys.schemas 
    WHERE name = 'Skin'
)
BEGIN
    EXEC('CREATE SCHEMA Skin');
END
GO