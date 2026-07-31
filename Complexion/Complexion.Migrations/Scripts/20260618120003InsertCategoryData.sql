IF NOT EXISTS (SELECT 1 FROM catalogue.Category)
BEGIN
    INSERT INTO Catalogue.Category (
		Name
	) VALUES(
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