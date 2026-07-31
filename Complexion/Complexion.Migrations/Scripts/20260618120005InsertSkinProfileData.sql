IF NOT EXISTS 
(
	SELECT 
	1 
	FROM dbo.SkinProfile
)
BEGIN
	INSERT INTO dbo.SkinProfile (
		SkinProfileId,
		ShadeId,
		UndertoneId,
		HasTint
	) VALUES (
		NEWID(),
		4,
		4,
		0
	)
END
GO