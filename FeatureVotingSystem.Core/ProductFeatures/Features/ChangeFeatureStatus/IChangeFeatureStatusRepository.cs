namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;

public interface IChangeFeatureStatusRepository
{
    Task<int> ChangeAsync(ChangeFeatureStatusRequest request);
}