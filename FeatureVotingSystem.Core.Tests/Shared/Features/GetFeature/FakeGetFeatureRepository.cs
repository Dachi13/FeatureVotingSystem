using FeatureVotingSystem.Core.ProductFeatures.Features;
using FeatureVotingSystem.Core.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Tests.Comments;

namespace FeatureVotingSystem.Core.Tests.Shared.Features.GetFeature;

public class FakeGetFeatureRepository : IGetFeatureRepository
{
    public async Task<Feature?> GetByIdAsync(int featureId)
    {
        var features = await FakeFeatureDbContext.GetListOfDummyFeatures();
        var firstOrDefault = features.FirstOrDefault(f => f.Id == featureId);
        return firstOrDefault;
    }
}