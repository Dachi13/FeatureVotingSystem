
namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;

public interface IChangeFeatureStatusService
{
    Task<int> ChangeAsync(ChangeFeatureStatusRequest request);
}