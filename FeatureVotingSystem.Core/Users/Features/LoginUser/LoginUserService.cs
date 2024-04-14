using FeatureVotingSystem.Core.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.Users.Features.LoginUser;

public class LoginUserService : ILoginUserService
{
    private readonly IGetUserByEmailRepository _getUserByEmail;
    private readonly ICheckUserPasswordRepository _checkUserPassword;

    public LoginUserService(IGetUserByEmailRepository getUserByEmail, ICheckUserPasswordRepository checkUserPassword)
    {
        _getUserByEmail = getUserByEmail;
        _checkUserPassword = checkUserPassword;
    }

    public async Task<int> LoginAsync(LoginRequest request)
    {
        var validation = UserValidations.ValidateLoginRequest(request);

        if (!validation.IsValid) throw new UserBadRequestException(validation.Errors.First().ErrorMessage);

        var user = await _getUserByEmail.FindByEmailAsync(request.Email);

        if (user is null)
            throw new UnauthorizedAccessException("E-mail or password is incorrect");

        var isCorrectPassword = await _checkUserPassword.CheckPasswordAsync(user, request.Password);

        if (!isCorrectPassword) throw new UnauthorizedAccessException("E-mail or password is incorrect");

        return user.Id;
    }
}