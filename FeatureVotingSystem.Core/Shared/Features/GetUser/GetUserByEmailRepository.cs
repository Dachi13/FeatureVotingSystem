using FeatureVotingSystem.Core.Users;
using Microsoft.AspNetCore.Identity;

namespace FeatureVotingSystem.Core.Shared.Features.GetUser;

public class GetUserByEmailRepository : IGetUserByEmailRepository
{
    private readonly UserManager<UserEntity> _userManager;

    public GetUserByEmailRepository(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserEntity?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
}