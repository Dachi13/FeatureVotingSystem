namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;

public interface IChangeFeaturePropertiesService
{
    Task<int> ChangeAsync(ChangeFeaturePropertiesRequest featurePropertiesRequest);
}