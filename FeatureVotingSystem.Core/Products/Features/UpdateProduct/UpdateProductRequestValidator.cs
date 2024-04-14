using FluentValidation;
using FeatureVotingSystem.Core.Shared;

namespace FeatureVotingSystem.Core.Products.Features.UpdateProduct;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
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

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is not passed")
            .Must(BeAValidId)
            .WithMessage(
                "Request contains invalid {PropertyName}: {PropertyValue}. {PropertyName} has to be greater than 0.");
    }

    private static bool BeAValidId(int id)
    {
        return id > 0;
    }
}