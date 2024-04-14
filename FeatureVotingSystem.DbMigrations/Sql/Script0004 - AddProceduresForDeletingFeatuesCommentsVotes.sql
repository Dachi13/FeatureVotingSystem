CREATE PROCEDURE SoftDeleteFeatures
    @featureId INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE Votes SET IsDeleted = 1 WHERE FeatureId = @featureId
            UPDATE Comments SET IsDeleted = 1 WHERE FeatureId = @featureId
            UPDATE Features SET StatusId = 3 WHERE Id = @featureId
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        THROW
        ROLLBACK TRANSACTION
    END CATCH
END
GO
