using FeatureVotingSystem.Core.ProductFeatures.Features.DeleteFeature;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.DeleteFeature;

public class FakeDeleteFeatureRepository : IDeleteFeatureRepository
{
    public async Task<int> ByIdAsync(int featureId)
    {
        return await Task.FromResult(1);
    }
}