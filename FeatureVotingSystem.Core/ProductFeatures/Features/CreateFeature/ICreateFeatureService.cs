namespace FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;

public interface ICreateFeatureService
{
    Task<int> CreateAsync(CreateFeatureRequest request);
}