using Microsoft.AspNetCore.Identity;

namespace FeatureVotingSystem.Core.Users.Features.RegisterUser;

public class AddRoleToUserRepository : IAddRoleToUserRepository
{
    private readonly UserManager<UserEntity> _userManager;

    public AddRoleToUserRepository(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> AddToRoleAsync(UserEntity entity, string user)
    {
        return await _userManager.AddToRoleAsync(entity, user);
    }
}