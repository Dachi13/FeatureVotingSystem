using FeatureVotingSystem.Core.ProductFeatures.Enums;
using FluentValidation;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;

public class ChangeFeatureStatusRequestValidator : AbstractValidator<ChangeFeatureStatusRequest>
{
    public ChangeFeatureStatusRequestValidator()
    {
        RuleFor(feature => feature)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Must(feature =>
                IsRegularStatus(feature)
                || IsRejectionStatus(feature))
            .WithMessage(
                "StatusId must be between 1 and 5 and not 3. When rejecting feature, only then rejection reason has to be provided and should be between 2 and 256 chars long");
    }

    private static bool IsRejectionStatus(ChangeFeatureStatusRequest feature)
    {
        return feature.StatusId == (int)Status.Rejected
               && !string.IsNullOrEmpty(feature.RejectionReason)
               && feature.RejectionReason.Length is > 2 and < 256;
    }

    private static bool IsRegularStatus(ChangeFeatureStatusRequest feature)
    {
        return feature.StatusId is >= (int)Status.Active and <= (int)Status.Completed and not (int)Status.Rejected 
            and not (int)Status.Deleted && string.IsNullOrEmpty(feature.RejectionReason);
    }
}