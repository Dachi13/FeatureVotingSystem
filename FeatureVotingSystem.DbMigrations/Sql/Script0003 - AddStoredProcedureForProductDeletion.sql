CREATE PROCEDURE dbo.spDeleteProduct
	@ProductId int
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE VOTES SET IsDeleted = 1 WHERE ProductId = @ProductId
			UPDATE Comments SET IsDeleted = 1 WHERE FeatureId IN (SELECT Id FROM Features WHERE ProductId = @ProductId)
			UPDATE Features SET StatusId = 3 WHERE ProductId = @ProductId
			UPDATE Products SET IsDeleted = 1 WHERE Id = @ProductId
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		THROW
		ROLLBACK TRANSACTION
	END CATCH
END
GO