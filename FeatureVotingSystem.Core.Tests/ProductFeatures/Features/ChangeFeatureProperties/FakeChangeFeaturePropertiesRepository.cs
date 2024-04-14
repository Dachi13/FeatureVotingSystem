using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.ChangeFeatureProperties;

public class FakeChangeFeaturePropertiesRepository : IChangeFeaturePropertiesRepository
{
    public async Task<int> ChangeAsync(ChangeFeaturePropertiesRequest featurePropertiesRequest)
    {
        return await Task.FromResult(1);
    }
}