using Microsoft.AspNetCore.Identity;

namespace FeatureVotingSystem.Core.Users.Features.RegisterUser;

public class RegisterUserRepository : IRegisterUserRepository
{
    private readonly UserManager<UserEntity> _userManager;

    public RegisterUserRepository(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IdentityResult> CreateAsync(UserEntity entity, string requestPassword)
    {
        return await _userManager.CreateAsync(entity, requestPassword);
    }
}