using FluentValidation;
using FeatureVotingSystem.Core.Shared; 

namespace FeatureVotingSystem.Core.Products.Features.CreateProduct;

public sealed class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(p => p.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(2, 30).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid")
            .Must(name => name.BeValidName()).WithMessage("{PropertyName} contains invalid characters");

        RuleFor(p => p.ShortDesc)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(2, 256).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid");
    }
}
