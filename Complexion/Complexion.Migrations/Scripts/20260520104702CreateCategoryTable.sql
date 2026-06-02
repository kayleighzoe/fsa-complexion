IF NOT EXISTS
(
    SELECT
        1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'catalogue'
        AND TABLE_NAME = 'Category'
)
BEGIN
    CREATE TABLE catalogue.Category
    (
        CategoryId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(25) NOT NULL
    );
END
GO