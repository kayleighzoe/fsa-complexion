IF NOT EXISTS (SELECT 1 FROM config.PriceTier)
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