using FeatureVotingSystem.Core.Shared.Models;
using FeatureVotingSystem.Shared.Entities.Exceptions.Reports;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Validations;

public static class ProductAuthorReportRequestValidationsHelper
{
    public static async Task<TimeSpanModel> ProductAuthorReportRequestValidations(int userId, ProductAuthorReportRequest productAuthorReportRequest, IGetProductRepository getProductRepository)
    {
        if (productAuthorReportRequest is null)
            throw new ProductAuthorReportBadRequest($"{nameof(productAuthorReportRequest)} can't be null");

        var validationResult = ReportValidations.ValidateReportRequestByProductAuthor(productAuthorReportRequest);

        if (!validationResult.IsValid)
            throw new ProductAuthorReportBadRequest(validationResult.Errors.First().ErrorMessage);

        Product? product = await getProductRepository.GetProductAsync(productAuthorReportRequest.ProductId);

        if (product == null || product.IsDeleted == 1) throw new ProductNotFoundException(productAuthorReportRequest.ProductId);

        if (product.UserId != userId)
            throw new ProductAuthorReportBadRequest($"Product with id:{productAuthorReportRequest.ProductId} doesn't belong to current user");

        TimeSpanModel? timeSpan = null;

        if (productAuthorReportRequest is ProductAuthorReportFilteredByTimeFrameRequest filteredRequest)
        {
            ReportValidations.ReportTimeFrameValidator(filteredRequest.ReportTimeFrame);

            timeSpan = TimeSpanHelper.GetTimeSpan(filteredRequest.ReportTimeFrame);
        }

        return timeSpan!;
    }
}
