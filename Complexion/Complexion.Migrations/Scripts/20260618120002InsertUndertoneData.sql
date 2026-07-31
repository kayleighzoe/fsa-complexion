IF NOT EXISTS (SELECT 1 FROM skin.Undertone)
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