using FluentValidation;
using FeatureVotingSystem.Core.Shared; 
    
namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;

public class ChangeFeatureRequestValidator : AbstractValidator<ChangeFeaturePropertiesRequest>
{
    public ChangeFeatureRequestValidator()
    {
        RuleFor(feature => feature.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid")
            .Must(name => name.BeValidName()).WithMessage("{PropertyName} contains invalid characters");

        RuleFor(feature => feature.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(1, 512).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid");
    }
}