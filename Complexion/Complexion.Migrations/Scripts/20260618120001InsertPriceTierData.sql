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