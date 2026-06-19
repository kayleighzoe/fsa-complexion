IF NOT EXISTS (SELECT 1 FROM skin.Shade)
BEGIN
	INSERT INTO skin.Shade (
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