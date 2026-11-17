IF NOT EXISTS 
(
        SELECT 
        1 
        FROM sys.key_constraints 
        WHERE name = 'UQ_SkinProfile_Combination'
)
BEGIN
    ALTER TABLE dbo.SkinProfile
    ADD CONSTRAINT UQ_SkinProfile_Combination UNIQUE (ShadeId, UndertoneId, HasTint);
END
GO