namespace FeatureVotingSystem.Core.Users.Features.LoginUser;

public interface ILoginUserService
{
    Task<int> LoginAsync(LoginRequest request);
}