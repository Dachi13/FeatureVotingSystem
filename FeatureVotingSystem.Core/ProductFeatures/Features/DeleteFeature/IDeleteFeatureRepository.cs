namespace FeatureVotingSystem.Core.ProductFeatures.Features.DeleteFeature;

public interface IDeleteFeatureRepository
{
    Task<int> ByIdAsync(int featureId);
}