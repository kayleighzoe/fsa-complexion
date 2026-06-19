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

IF NOT EXISTS (SELECT 1 FROM skin.Undertone)
BEGIN
    INSERT INTO skin.Undertone (
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


IF NOT EXISTS (SELECT 1 FROM config.PriceTier)
BEGIN
    INSERT INTO config.PriceTier (
		Name
	) VALUES(
		'High-End'
	),
    (
		'Drug-Store'
	);
END
GO