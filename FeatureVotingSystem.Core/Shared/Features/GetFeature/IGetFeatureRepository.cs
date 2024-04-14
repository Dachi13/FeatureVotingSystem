using FeatureVotingSystem.Core.ProductFeatures.Features;

namespace FeatureVotingSystem.Core.Shared.Features.GetFeature;

public interface IGetFeatureRepository
{
    Task<Feature?> GetByIdAsync(int featureId);
}