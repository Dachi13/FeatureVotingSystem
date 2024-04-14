using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests;
using FeatureVotingSystem.Shared.Entities.Exceptions.Reports;
using FluentValidation.Results;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Validations;

public static class ReportValidations
{
    public static ValidationResult ValidateReportRequestByProductAuthor(ProductAuthorReportRequest reportRequestByProductAuthor)
    {
        var validator = new ReportRequestByProductAuthorValidator();
        var result = validator.Validate(reportRequestByProductAuthor);

        return result;
    }

    public static void ReportTimeFrameValidator(ReportTimeFrame reportTimeFrame)
    {
        bool isValidProperty = Enum.IsDefined(typeof(ReportTimeFrame), reportTimeFrame);

        if (!isValidProperty)
            throw new TimeFrameBadRequestException(reportTimeFrame.ToString());
    }
}
