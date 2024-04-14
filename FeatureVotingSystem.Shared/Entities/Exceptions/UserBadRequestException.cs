namespace FeatureVotingSystem.Shared.Entities.Exceptions;

public class UserBadRequestException : BadRequestException
{
    public UserBadRequestException(string message) : base(message)
    {
    }
}