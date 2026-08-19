IF NOT EXISTS
(
    SELECT
        1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = 'SkinProfile'
)
BEGIN
    CREATE TABLE dbo.SkinProfile
    (
        SkinProfileId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        ShadeId INT NOT NULL,
        UndertoneId INT NOT NULL,
        HasTint BIT NOT NULL DEFAULT(0),
        CONSTRAINT FK_SkinProfile_Shade FOREIGN KEY (ShadeId) REFERENCES skin.Shade(ShadeId),
        CONSTRAINT FK_SkinProfile_Undertone FOREIGN KEY (UndertoneId) REFERENCES skin.Undertone(UndertoneId)
    );
END
GO