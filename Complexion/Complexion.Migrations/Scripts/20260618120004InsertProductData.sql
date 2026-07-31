IF NOT EXISTS 
(
	SELECT 
	1 
	FROM dbo.Product
)
BEGIN
	INSERT INTO dbo.Product (
		ProductId,
		CategoryId,
		PriceTierId,
		Name,
		Brand,
		ShadeName
	) VALUES (
		NEWID(),
		1,
		1,
		'Studio Fix Fluid',
		'MAC',
		'nc44.5'
	),
	(
		NEWID(),
		4,
		2,
		'Fitme Spot Concealer',
		'Maybelline',
		'75'
	)
END
GO