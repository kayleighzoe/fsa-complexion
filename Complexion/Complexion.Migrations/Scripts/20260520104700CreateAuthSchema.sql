IF NOT EXISTS 
(
    SELECT 
        1 
    FROM sys.schemas 
    WHERE name = 'Auth'
 )
BEGIN
    EXEC('CREATE SCHEMA Auth');
END
GO