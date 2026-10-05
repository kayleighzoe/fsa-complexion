IF NOT EXISTS
(
    SELECT
        1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = 'ProductRecommendation'
)
BEGIN
    CREATE TABLE dbo.ProductRecommendation
    (
        RecommendationId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        UserId UNIQUEIDENTIFIER NOT NULL,
        ProductId UNIQUEIDENTIFIER NOT NULL,
        SkinProfileId UNIQUEIDENTIFIER NOT NULL,
        Comment VARCHAR(255),
        CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_ProductRecommendation_User FOREIGN KEY (UserId) REFERENCES auth.[User](UserId),
        CONSTRAINT FK_ProductRecommendation_Product FOREIGN KEY (ProductId) REFERENCES dbo.Product(ProductId),
        CONSTRAINT FK_ProductRecommendation_SkinProfile FOREIGN KEY (SkinProfileId) REFERENCES dbo.SkinProfile(SkinProfileId)
    );
END
GO