IF NOT EXISTS
(
    SELECT
        1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'auth'
        AND TABLE_NAME = 'User'
)
BEGIN
    CREATE TABLE auth.[User]
    (
        UserId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        SkinProfileId UNIQUEIDENTIFIER NOT NULL,
        FirstName VARCHAR(50) NOT NULL,
        Surname VARCHAR(50) NOT NULL,
        Username VARCHAR(50) NOT NULL,
        Email VARCHAR(50) NOT NULL,
        PasswordHash VARCHAR(255) NOT NULL,
        CONSTRAINT FK_User_SkinProfile FOREIGN KEY (SkinProfileId) REFERENCES dbo.SkinProfile(SkinProfileId)
    );
END
GO