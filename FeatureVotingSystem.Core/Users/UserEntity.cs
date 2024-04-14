using Microsoft.AspNetCore.Identity;

namespace FeatureVotingSystem.Core.Users;

public class UserEntity : IdentityUser<int>
{
    public required string Surname { get; init; }
    public DateTime DateOfCreation { get; } = DateTime.Now;
}