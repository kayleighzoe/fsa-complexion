IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name LIKE 'skin.Shade'
)
BEGIN
    CREATE TABLE skin.Shade (
        ShadeId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        Name VARCHAR(25) NOT NULL
    );
END
GO

IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name LIKE 'skin.Undertone'
)
BEGIN
    CREATE TABLE skin.Undertone (
        UndertoneId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        Name VARCHAR(25) NOT NULL
    );
END
GO