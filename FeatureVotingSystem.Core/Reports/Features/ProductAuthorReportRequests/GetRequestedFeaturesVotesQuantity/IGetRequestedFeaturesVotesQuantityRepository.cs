namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesVotesQuantity;

public interface IGetRequestedFeaturesVotesQuantityRepository
{
    Task<IEnumerable<FeatureVotesQuantityResponse>> GetRequestedFeaturesVotesQuantityAsync(int productId, TimeSpanModel timeSpanModel = null);
}
