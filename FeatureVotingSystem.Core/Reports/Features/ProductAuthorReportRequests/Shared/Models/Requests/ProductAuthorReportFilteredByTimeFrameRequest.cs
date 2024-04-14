namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests
{
    public sealed class ProductAuthorReportFilteredByTimeFrameRequest : ProductAuthorReportRequest
    {
        public ReportTimeFrame ReportTimeFrame { get; set; }
    }
}
