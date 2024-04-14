CREATE PROCEDURE dbo.spCheckIfProductWithGivenNameAlreadyExists
    @Name NVARCHAR(30)
AS
BEGIN
    SELECT Count(*) 
    FROM Products 
    WHERE LOWER([Name]) = LOWER(@Name)
END
GO
