using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests;
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Validations;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesVotesQuantity;

public class GetRequestedFeaturesVotesQuantityService : IGetRequestedFeaturesVotesQuantityService
{
    private readonly IGetRequestedFeaturesVotesQuantityRepository _getRequestedFeaturesVotesQuantityRepository;
    private readonly IGetProductRepository _getProductRepository;

    public GetRequestedFeaturesVotesQuantityService(
        IGetRequestedFeaturesVotesQuantityRepository getRequestedFeaturesVotesQuantityRepository,
        IGetProductRepository getProductRepository)
    {
        _getRequestedFeaturesVotesQuantityRepository = getRequestedFeaturesVotesQuantityRepository;
        _getProductRepository = getProductRepository;
    }
    public async Task<IEnumerable<FeatureVotesQuantityResponse>> GetRequestedFeaturesVotesQuantityAsync(int userId, ProductAuthorReportRequest productAuthorReportRequest)
    {
        TimeSpanModel timeSpan = await ProductAuthorReportRequestValidationsHelper.ProductAuthorReportRequestValidations(userId, productAuthorReportRequest, _getProductRepository);

        IEnumerable<FeatureVotesQuantityResponse> result = timeSpan is null ?
            await _getRequestedFeaturesVotesQuantityRepository.GetRequestedFeaturesVotesQuantityAsync(productAuthorReportRequest.ProductId) :
            await _getRequestedFeaturesVotesQuantityRepository.GetRequestedFeaturesVotesQuantityAsync(productAuthorReportRequest.ProductId, timeSpan);

        return result;
    }
}
