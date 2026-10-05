IF NOT EXISTS
(
    SELECT
        1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'Catalogue'
        AND TABLE_NAME = 'Category'
)
BEGIN
    CREATE TABLE catalogue.Category
    (
        CategoryId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(25) NOT NULL
    );
END
GO

IF NOT EXISTS 
(
	SELECT 
		1 
	FROM catalogue.Category
)
BEGIN
    INSERT INTO Catalogue.Category
	(
		Name
	) 
	VALUES
	(
		'High-coverage Foundation'
	),
	(
		'Light-coverage Foundation'
	),
	(
		'Skin Tint'
	),
	(
		'Corrective concealer'
	),
    (
		'Highlighing Concealer'
	);
END
GO