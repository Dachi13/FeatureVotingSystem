namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesVotesQuantity;

public class FeatureVotesQuantityResponse
{
    public string ProductName { get; set; }
    public string FeatureName { get; set; }
    public int UpVotes { get; set; }
    public int DownVotes { get; set; }
}