using FeatureVotingSystem.Core.Users.Features.LoginUser;
using FeatureVotingSystem.Core.Users.Features.RegisterUser;
using FluentValidation.Results;

namespace FeatureVotingSystem.Core.Users.Features;

public class UserValidations
{
    public static ValidationResult ValidateCreateUserRequest(RegisterUserRequest user)
    {
        var validator = new RegisterUserRequestValidator();
        var result = validator.Validate(user);

        return result;
    }

    public static ValidationResult ValidateLoginRequest(LoginRequest request)
    {
        var validator = new LoginRequestValidator();
        var result = validator.Validate(request);

        return result;
    }
}