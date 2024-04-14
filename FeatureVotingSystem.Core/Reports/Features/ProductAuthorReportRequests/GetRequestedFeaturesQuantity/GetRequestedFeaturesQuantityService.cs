using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests;
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Validations;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesQuantity;

public class GetRequestedFeaturesQuantityService : IGetRequestedFeaturesQuantityService
{
    private readonly IGetRequestedFeaturesQuantityRepository _getRequestedFeaturesQuantityRepository;
    private readonly IGetProductRepository _getProductRepository;
    public GetRequestedFeaturesQuantityService(IGetRequestedFeaturesQuantityRepository getRequestedFeaturesQuantityRepository, IGetProductRepository getProductRepository)
    {
        _getRequestedFeaturesQuantityRepository = getRequestedFeaturesQuantityRepository;
        _getProductRepository = getProductRepository;
    }
    public async Task<int> GetRequestedFeaturesQuantityAsync(int userId, ProductAuthorReportRequest productAuthorReportRequest)
    {
        TimeSpanModel timeSpan = await ProductAuthorReportRequestValidationsHelper.ProductAuthorReportRequestValidations(userId, productAuthorReportRequest, _getProductRepository);

        int result = timeSpan is null ?
            await _getRequestedFeaturesQuantityRepository.GetRequestedFeaturesQuantityAsync(productAuthorReportRequest.ProductId) :
            await _getRequestedFeaturesQuantityRepository.GetRequestedFeaturesQuantityAsync(productAuthorReportRequest.ProductId, timeSpan);

        return result;
    }
}
