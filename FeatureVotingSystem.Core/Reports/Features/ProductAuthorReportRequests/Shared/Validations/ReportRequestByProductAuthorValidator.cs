using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests;
using FluentValidation;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Validations;

public sealed class ReportRequestByProductAuthorValidator : AbstractValidator<ProductAuthorReportRequest>
{
    public ReportRequestByProductAuthorValidator()
    {
        RuleFor(r => r.ProductId)
            .GreaterThan(0).WithMessage("{PropertyName}:{PropertyValue} must be greater than 0");
    }
}
