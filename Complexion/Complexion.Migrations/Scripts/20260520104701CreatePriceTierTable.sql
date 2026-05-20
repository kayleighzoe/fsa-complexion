IF NOT EXISTS
(
    SELECT
        1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'catalogue'
        AND TABLE_NAME = 'PriceTier'
)
BEGIN
    CREATE TABLE catalogue.PriceTier
    (
        PriceTierId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(15) NOT NULL
    );
END
GO