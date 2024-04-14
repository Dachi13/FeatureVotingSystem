﻿using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesVotesQuantity;

public interface IGetRequestedFeaturesVotesQuantityService
{
    Task<IEnumerable<FeatureVotesQuantityResponse>> GetRequestedFeaturesVotesQuantityAsync(int userId, ProductAuthorReportRequest reportRequestByProductAuthor);
}
