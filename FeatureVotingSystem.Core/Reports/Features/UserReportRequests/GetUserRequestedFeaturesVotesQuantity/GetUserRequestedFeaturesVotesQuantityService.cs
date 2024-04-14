
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Validations;
using FeatureVotingSystem.Shared.Entities.Exceptions.Reports;

namespace FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetUserRequestedFeaturesVotesQuantity;

public class GetUserRequestedFeaturesVotesQuantityService : IGetUserRequestedFeaturesVotesQuantityService
{
    private readonly IGetUserRequestedFeaturesVotesQuantityRepository _getUserRequestedFeaturesVotesQuantityRepository;
    public GetUserRequestedFeaturesVotesQuantityService(IGetUserRequestedFeaturesVotesQuantityRepository getUserRequestedFeaturesVotesQuantityRepository)
    {
        _getUserRequestedFeaturesVotesQuantityRepository = getUserRequestedFeaturesVotesQuantityRepository;
    }
    public async Task<IEnumerable<UserRequestedFeatureVotesQuantityResponse>> GetUserRequestedFeaturesVotesQuantityAsync(int userId, UserRequestedFeatureVotesQuantityRequest userRequestedFeatureVotesQuantityRequest)
    {
        if (userRequestedFeatureVotesQuantityRequest is null)
            throw new UserRequestedFeatureVotesQuantityBadRequestException();

        ReportValidations.ReportTimeFrameValidator(userRequestedFeatureVotesQuantityRequest.reportTimeFrame);

        TimeSpanModel timeSpan = TimeSpanHelper.GetTimeSpan(userRequestedFeatureVotesQuantityRequest.reportTimeFrame);

        IEnumerable<UserRequestedFeatureVotesQuantityResponse> result = await _getUserRequestedFeaturesVotesQuantityRepository.GetUserRequestedFeaturesVotesQuantity(userId, timeSpan);

        return result;
    }

    public async Task<IEnumerable<UserRequestedFeatureVotesQuantityResponse>> GetUserRequestedFeaturesVotesQuantityAsync(int userId)
    {
        IEnumerable<UserRequestedFeatureVotesQuantityResponse> result = await _getUserRequestedFeaturesVotesQuantityRepository.GetUserRequestedFeaturesVotesQuantity(userId);

        return result;
    }
}
