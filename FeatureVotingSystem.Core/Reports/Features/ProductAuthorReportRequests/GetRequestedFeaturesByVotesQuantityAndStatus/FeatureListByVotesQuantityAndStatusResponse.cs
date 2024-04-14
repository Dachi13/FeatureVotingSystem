namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesByVotesQuantityAndStatus;

public class FeatureListByVotesQuantityAndStatusResponse
{
    public string FeatureName { get; set; }
    public string Status { get; set; }
    public int VotesQuantity { get; set; }
}
