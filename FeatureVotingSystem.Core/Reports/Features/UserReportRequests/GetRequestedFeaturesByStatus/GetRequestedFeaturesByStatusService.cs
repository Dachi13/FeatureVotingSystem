using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Validations;
using FeatureVotingSystem.Shared.Entities.Exceptions.Reports;

namespace FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetRequestedFeaturesByStatus;

public class GetRequestedFeaturesByStatusService : IGetRequestedFeaturesByStatusService
{
    private readonly IGetRequestedFeaturesByStatusRepository _getRequestedFeaturesByStatusRepository;

    public GetRequestedFeaturesByStatusService(IGetRequestedFeaturesByStatusRepository getRequestedFeaturesByStatusRepository)
    {
        _getRequestedFeaturesByStatusRepository = getRequestedFeaturesByStatusRepository;
    }

    public async Task<IEnumerable<FeatureListByStatusResponse>> GetRequestedFeaturesByStatusAsync(int userId, FeatureListByStatusRequest featureListByStatusRequest)
    {
        if (featureListByStatusRequest is null)
            throw new FeatureListByStatusBadRequestException();

        ReportValidations.ReportTimeFrameValidator(featureListByStatusRequest.reportTimeFrame);

        TimeSpanModel timeSpan = TimeSpanHelper.GetTimeSpan(featureListByStatusRequest.reportTimeFrame);

        IEnumerable<FeatureListByStatusResponse> result = await _getRequestedFeaturesByStatusRepository.GetRequestedFeaturesByStatusAsync(userId, timeSpan);

        return result;
    }

    public async Task<IEnumerable<FeatureListByStatusResponse>> GetRequestedFeaturesByStatusAsync(int userId)
    {
        IEnumerable<FeatureListByStatusResponse> result = await _getRequestedFeaturesByStatusRepository.GetRequestedFeaturesByStatusAsync(userId);

        return result;

    }
}
