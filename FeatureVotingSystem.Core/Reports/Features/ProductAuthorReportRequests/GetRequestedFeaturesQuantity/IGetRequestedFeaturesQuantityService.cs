using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesQuantity;

public interface IGetRequestedFeaturesQuantityService
{
    Task<int> GetRequestedFeaturesQuantityAsync(int userId, ProductAuthorReportRequest reportRequestByProductAuthor);
}
