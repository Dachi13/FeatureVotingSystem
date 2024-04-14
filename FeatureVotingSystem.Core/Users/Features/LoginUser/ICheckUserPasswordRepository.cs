namespace FeatureVotingSystem.Core.Users.Features.LoginUser;

public interface ICheckUserPasswordRepository
{
    Task<bool> CheckPasswordAsync(UserEntity user, string requestPassword);
}