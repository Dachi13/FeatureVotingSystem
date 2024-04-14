using Microsoft.AspNetCore.Identity;

namespace FeatureVotingSystem.Core.Users.Features.RegisterUser;

public interface IAddRoleToUserRepository
{
    Task<IdentityResult> AddToRoleAsync(UserEntity entity, string user);
}