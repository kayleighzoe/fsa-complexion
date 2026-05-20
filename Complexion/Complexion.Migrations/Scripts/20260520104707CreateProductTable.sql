IF NOT EXISTS
(
    SELECT
        1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = 'Product'
)
BEGIN
    CREATE TABLE dbo.Product
    (
        ProductId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        CategoryId INT NOT NULL,
        PriceTierId INT NOT NULL,
        Name VARCHAR(50) NOT NULL,
        Brand VARCHAR(50) NOT NULL,
        ShadeName VARCHAR(20) NOT NULL,
        CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES catalogue.Category(CategoryId),
        CONSTRAINT FK_Product_PriceTier FOREIGN KEY (PriceTierId) REFERENCES catalogue.PriceTier(PriceTierId)
    );
END
GO