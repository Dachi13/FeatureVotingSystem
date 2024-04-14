using FeatureVotingSystem.Core.Users;

namespace FeatureVotingSystem.Core.Shared.Features.GetUser;

public interface IGetUserByEmailRepository
{
    public Task<UserEntity?> FindByEmailAsync(string email);
}