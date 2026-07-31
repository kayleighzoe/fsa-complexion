IF NOT EXISTS
(
	SELECT
	1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_SCHEMA = 'Skin'
		AND TABLE_NAME = 'Shade'
)
BEGIN
	CREATE TABLE skin.Shade
	(
		ShadeId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		Name VARCHAR(25) NOT NULL
	);
END
GO

IF NOT EXISTS (SELECT 1 FROM skin.Shade)
BEGIN
	INSERT INTO Skin.Shade (
		Name
	) VALUES (
		'Fair'
	),
	(
		'Light'
	),
	(
		'Medium'
	),
	(
		'Tan'
	),
	(
		'Deep'
	),
	(
		'Very deep'
	);
END
GO