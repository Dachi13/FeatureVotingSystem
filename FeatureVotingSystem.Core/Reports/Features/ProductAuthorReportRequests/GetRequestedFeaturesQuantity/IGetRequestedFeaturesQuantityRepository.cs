namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesQuantity;

public interface IGetRequestedFeaturesQuantityRepository
{
    Task<int> GetRequestedFeaturesQuantityAsync(int productId, TimeSpanModel timeSpanModel = null);
}
