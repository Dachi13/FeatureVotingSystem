namespace FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetRequestedFeaturesByStatus;

public interface IGetRequestedFeaturesByStatusService
{
    Task<IEnumerable<FeatureListByStatusResponse>> GetRequestedFeaturesByStatusAsync(int userId, FeatureListByStatusRequest featureListByStatusRequest);
    Task<IEnumerable<FeatureListByStatusResponse>> GetRequestedFeaturesByStatusAsync(int userId);
}
