using Microsoft.AspNetCore.Identity;

namespace FeatureVotingSystem.Core.Users.Features.LoginUser;

public class CheckUserPasswordRepository : ICheckUserPasswordRepository
{
    private readonly UserManager<UserEntity> _userManager;

    public CheckUserPasswordRepository(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CheckPasswordAsync(UserEntity user, string requestPassword)
    {
        return await _userManager.CheckPasswordAsync(user, requestPassword);
    }
}