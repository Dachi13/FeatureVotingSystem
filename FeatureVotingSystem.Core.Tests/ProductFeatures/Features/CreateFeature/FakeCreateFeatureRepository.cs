using FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.CreateFeature;

public class FakeCreateFeatureRepository : ICreateFeatureRepository
{
    public async Task<int> CreateAsync(CreateFeatureRequest request)
    {
        return await Task.FromResult(1);
    }

    public async Task<int> GetTotalFeaturesUploadedByUserSinceDateAsync(int userId, int productId, DateTime dateLimit)
    {
        return userId % 2 == 0
            ? await Task.FromResult(2)
            : await Task.FromResult(10);
    }
}