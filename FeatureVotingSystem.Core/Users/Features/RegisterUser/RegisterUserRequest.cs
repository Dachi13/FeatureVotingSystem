namespace FeatureVotingSystem.Core.Users.Features.RegisterUser;

public class RegisterUserRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}