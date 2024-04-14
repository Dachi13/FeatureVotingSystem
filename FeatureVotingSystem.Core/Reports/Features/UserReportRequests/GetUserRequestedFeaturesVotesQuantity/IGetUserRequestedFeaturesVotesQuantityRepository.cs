namespace FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetUserRequestedFeaturesVotesQuantity;

public interface IGetUserRequestedFeaturesVotesQuantityRepository
{
    Task<IEnumerable<UserRequestedFeatureVotesQuantityResponse>> GetUserRequestedFeaturesVotesQuantity(int userId, TimeSpanModel timeSpanModel = null);
}
