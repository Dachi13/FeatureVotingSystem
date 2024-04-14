using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.ChangeFeatureStatus;

public class FakeChangeFeatureStatusRepository : IChangeFeatureStatusRepository
{
    public async Task<int> ChangeAsync(ChangeFeatureStatusRequest request)
    {
        return await Task.FromResult(1);
    }
}