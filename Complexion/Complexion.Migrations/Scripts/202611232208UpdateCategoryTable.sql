IF EXISTS
(
    SELECT
        1
    FROM catalogue.Category
    WHERE Name = 'Highlighing Concealer'
)
BEGIN
    UPDATE catalogue.Category
    SET Name = 'Highlighting Concealer'
    WHERE Name = 'Highlighing Concealer';
END
GO