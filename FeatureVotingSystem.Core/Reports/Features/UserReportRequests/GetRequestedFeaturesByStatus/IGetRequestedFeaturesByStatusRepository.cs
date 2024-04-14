namespace FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetRequestedFeaturesByStatus;

public interface IGetRequestedFeaturesByStatusRepository
{
    Task<IEnumerable<FeatureListByStatusResponse>> GetRequestedFeaturesByStatusAsync(int userId, TimeSpanModel timeSpanModel = null);
}
