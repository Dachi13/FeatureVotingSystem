namespace FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetUserRequestedFeaturesVotesQuantity;

public class UserRequestedFeatureVotesQuantityResponse
{
    public string ProductName { get; set; }
    public string FeatureName { get; set; }
    public int UpVotes { get; set; }
    public int DownVotes { get; set; }
}
