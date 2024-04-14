namespace FeatureVotingSystem.Shared.Entities.Exceptions.Reports;

public class ProductAuthorReportBadRequest : BadRequestException
{
    public ProductAuthorReportBadRequest(string message) : base(message)
    {
    }
}
