using FeatureVotingSystem.Core.Shared;
using FluentValidation;

namespace FeatureVotingSystem.Core.Users.Features.LoginUser;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(1, 256).WithMessage("Length ({PropertyName}) of {PropertyName} is Invalid")
            .Must(email => email.BeAValidEmail()).WithMessage("{PropertyValue} is not a valid email address");
    }
}