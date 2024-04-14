﻿namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesByVotesQuantityAndStatus;

public interface IGetRequestedFeaturesByVotesQuantityAndStatusRepository
{
    Task<IEnumerable<FeatureListByVotesQuantityAndStatusResponse>> GetRequestedFeaturesByVotesQuantityAndStatusAsync(int productId, TimeSpanModel timeSpanModel = null);
}
