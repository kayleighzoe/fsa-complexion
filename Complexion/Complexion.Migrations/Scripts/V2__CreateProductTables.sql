IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name LIKE 'product.Brand' 
)
BEGIN
    CREATE TABLE product.Brand (
        BrandId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        Name VARCHAR(25) NOT NULL
    );
END
GO

IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name LIKE 'product.Category'
)
BEGIN
    CREATE TABLE product.Category (
        CategoryId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        Name VARCHAR(25) NOT NULL
    );
END
GO

IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name LIKE 'product.PriceTier'
)
BEGIN
    CREATE TABLE product.PriceTier (
        PriceTierId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        Name VARCHAR(15) NOT NULL
    );
END
GO