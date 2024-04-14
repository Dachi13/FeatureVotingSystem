using Microsoft.AspNetCore.Identity;

namespace FeatureVotingSystem.Core.Users.Features.RegisterUser;

public interface IRegisterUserRepository
{
    Task<IdentityResult> CreateAsync(UserEntity entity, string requestPassword);
}