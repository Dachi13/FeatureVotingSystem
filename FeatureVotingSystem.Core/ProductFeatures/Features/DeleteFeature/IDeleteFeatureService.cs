namespace FeatureVotingSystem.Core.ProductFeatures.Features.DeleteFeature;

public interface IDeleteFeatureService
{
    Task<int> ByIdAsync(int featureId, int userId);
}