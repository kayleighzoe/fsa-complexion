IF NOT EXISTS
(
    SELECT
        1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'Config'
        AND TABLE_NAME = 'PriceTier'
)
BEGIN
    CREATE TABLE config.PriceTier
    (
        PriceTierId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(15) NOT NULL
    );
END
GO

IF NOT EXISTS 
(
    SELECT 
    1 
    FROM config.PriceTier
)
BEGIN
    INSERT INTO Config.PriceTier (
		Name
	) VALUES(
		'High-End'
	),
    (
		'Drug-Store'
	);
END
GO