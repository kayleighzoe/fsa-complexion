INSERT INTO dbo.SkinProfile (ShadeId, UndertoneId, HasTint)
SELECT
    shade.ShadeId,
    undertone.UndertoneId,
    tint.HasTint
FROM skin.Shade shade
CROSS JOIN skin.Undertone undertone
CROSS JOIN (VALUES (CAST(0 AS BIT)), (CAST(1 AS BIT))) AS tint (HasTint)
WHERE NOT EXISTS
(
    SELECT
        1
    FROM dbo.SkinProfile existing
    WHERE existing.ShadeId = shade.ShadeId
        AND existing.UndertoneId = undertone.UndertoneId
        AND existing.HasTint = tint.HasTint
);
GO