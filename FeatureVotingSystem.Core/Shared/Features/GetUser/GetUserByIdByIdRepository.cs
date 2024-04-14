using FeatureVotingSystem.Core.Users;
using Microsoft.AspNetCore.Identity;

namespace FeatureVotingSystem.Core.Shared.Features.GetUser;

public class GetUserByIdByIdRepository : IGetUserByIdRepository
{
    private readonly UserManager<UserEntity> _userManager;

    public GetUserByIdByIdRepository(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserEntity?> FindByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
}