namespace FeatureVotingSystem.Core.Users.Features.RegisterUser;

public interface IRegisterUserService
{
    Task RegisterAsync(RegisterUserRequest request);
}