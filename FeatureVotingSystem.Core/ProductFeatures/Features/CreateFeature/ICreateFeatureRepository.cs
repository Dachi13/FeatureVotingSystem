namespace FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;

public interface ICreateFeatureRepository
{
    Task<int> CreateAsync(CreateFeatureRequest request);
    Task<int> GetTotalFeaturesUploadedByUserSinceDateAsync(int userId, int productId, DateTime dateLimit);
}