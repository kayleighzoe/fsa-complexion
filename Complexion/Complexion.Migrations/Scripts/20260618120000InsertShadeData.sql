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