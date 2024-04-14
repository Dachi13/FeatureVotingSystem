using FluentValidation;
using FeatureVotingSystem.Core.Shared; 

namespace FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;

public class CreateFeatureRequestValidator : AbstractValidator<CreateFeatureRequest>
{
    public CreateFeatureRequestValidator()
    {
        RuleFor(feature => feature.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid")
            .Must(name => name.BeValidName()).WithMessage("Name contains invalid characters");

        RuleFor(feature => feature.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(1, 512).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid");
    }
}