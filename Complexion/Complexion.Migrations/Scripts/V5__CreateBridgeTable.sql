IF NOT EXISTS (
	SELECT * FROM sys.tables WHERE name LIKE 'ProductRecommendation'
)
BEGIN
	CREATE TABLE ProductRecommendation (
		RecommendationId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
		UserId UNIQUEIDENTIFIER NOT NULL,
		ProductId UNIQUEIDENTIFIER NOT NULL,
		SkinProfileId UNIQUEIDENTIFIER NOT NULL,
		Comment VARCHAR(255),
		CreatedAt DATETIME NOT NULL,
		CONSTRAINT FK_ProductRecommendation_User FOREIGN KEY (UserId) REFERENCES Users(UserId),
		CONSTRAINT FK_ProductRecommendation_Product FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
		CONSTRAINT FK_ProductRecommendation_SkinProfile FOREIGN KEY (SkinProfileId) REFERENCES SkinProfile(SkinProfileId)
	);
END
GO