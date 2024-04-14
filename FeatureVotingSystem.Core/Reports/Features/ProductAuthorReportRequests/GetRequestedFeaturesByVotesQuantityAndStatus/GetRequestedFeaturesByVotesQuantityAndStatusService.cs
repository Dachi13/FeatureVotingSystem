using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests;
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Validations;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesByVotesQuantityAndStatus;

public class GetRequestedFeaturesByVotesQuantityAndStatusService : IGetRequestedFeaturesByVotesQuantityAndStatusService
{
    private readonly IGetRequestedFeaturesByVotesQuantityAndStatusRepository _getRequestedFeaturesByVotesQuantityAndStatusRepository;
    private readonly IGetProductRepository _getProductRepository;

    public GetRequestedFeaturesByVotesQuantityAndStatusService(
        IGetRequestedFeaturesByVotesQuantityAndStatusRepository getRequestedFeaturesByVotesQuantityAndStatusRepository, 
        IGetProductRepository getProductRepository)
    {
        _getRequestedFeaturesByVotesQuantityAndStatusRepository = getRequestedFeaturesByVotesQuantityAndStatusRepository;
        _getProductRepository = getProductRepository;
    }

    public async Task<IEnumerable<FeatureListByVotesQuantityAndStatusResponse>> GetRequestedFeaturesByVotesQuantityAndStatusAsync(int userId, ProductAuthorReportRequest productAuthorReportRequest)
    {
        TimeSpanModel timeSpan = await ProductAuthorReportRequestValidationsHelper.ProductAuthorReportRequestValidations(userId, productAuthorReportRequest, _getProductRepository);

        IEnumerable<FeatureListByVotesQuantityAndStatusResponse> result = timeSpan is null ?
            await _getRequestedFeaturesByVotesQuantityAndStatusRepository.GetRequestedFeaturesByVotesQuantityAndStatusAsync(productAuthorReportRequest.ProductId) :
            await _getRequestedFeaturesByVotesQuantityAndStatusRepository.GetRequestedFeaturesByVotesQuantityAndStatusAsync(productAuthorReportRequest.ProductId, timeSpan);

        return result;
    }
}
