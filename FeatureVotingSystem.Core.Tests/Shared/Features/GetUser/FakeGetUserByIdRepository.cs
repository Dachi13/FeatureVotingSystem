using FeatureVotingSystem.Core.Shared.Features.GetUser;
using FeatureVotingSystem.Core.Users;

namespace FeatureVotingSystem.Core.Tests.Shared.Features.GetUser;

public class FakeGetUserByIdRepository : IGetUserByIdRepository
{
    public async Task<UserEntity?> FindByIdAsync(string id)
    {
        return await Task.FromResult(new UserEntity { Surname = "Surname" });
    }
}