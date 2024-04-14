using FeatureVotingSystem.Core.Users;

namespace FeatureVotingSystem.Core.Shared.Features.GetUser;

public interface IGetUserByIdRepository
{
    Task<UserEntity?> FindByIdAsync(string id);
}