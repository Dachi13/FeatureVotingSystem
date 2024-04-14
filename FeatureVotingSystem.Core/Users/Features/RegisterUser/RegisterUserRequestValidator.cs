using System.Text.RegularExpressions;
using FeatureVotingSystem.Core.Shared;
using FluentValidation;

namespace FeatureVotingSystem.Core.Users.Features.RegisterUser;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(user => user.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(2, 30).WithMessage("Length ({TotalLength}) of {UserName} is Invalid")
            .Must(name => name.BeValidName()).WithMessage("{PropertyName} is invalid");

        RuleFor(user => user.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(1, 256).WithMessage("Length ({PropertyName}) of {PropertyName} is Invalid")
            .Must(email => email.BeAValidEmail()).WithMessage("{PropertyName} is not a valid email address");

        RuleFor(user => user.Surname)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(1, 256).WithMessage("Length ({PropertyName}) of {PropertyName} is Invalid")
            .Must(BeAValidSurname).WithMessage("Surname cannot contain numbers or invalid characters");
    }

    private static bool BeAValidSurname(string surname)
    {
        var surnamePattern = "^[a-zA-Z._-]+$";

        return Regex.IsMatch(surname, surnamePattern);
    }
}