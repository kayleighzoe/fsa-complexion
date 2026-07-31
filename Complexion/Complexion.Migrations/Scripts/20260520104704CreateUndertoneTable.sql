IF NOT EXISTS
(
	SELECT
	1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_SCHEMA = 'Skin'
		AND TABLE_NAME = 'Undertone'
)
BEGIN
	CREATE TABLE skin.Undertone
	(
		UndertoneId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		Name VARCHAR(25) NOT NULL
	);
END
GO

IF NOT EXISTS 
(
	SELECT 
	1 
	FROM skin.Undertone
)
BEGIN
    INSERT INTO Skin.Undertone (
		Name
	) VALUES(
		'Cool'
	),
    (
		'Cool-Neutral'
	),
    (
		'True Neutral'
	),
    (
		'Warm-Neutral'
	),
    (
		'Warm'
	);
END
GO