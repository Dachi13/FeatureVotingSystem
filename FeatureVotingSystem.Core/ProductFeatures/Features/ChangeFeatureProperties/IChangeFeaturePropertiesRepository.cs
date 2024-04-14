namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;

public interface IChangeFeaturePropertiesRepository
{
    Task<int> ChangeAsync(ChangeFeaturePropertiesRequest featurePropertiesRequest);
}