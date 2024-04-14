namespace FeatureVotingSystem.Shared.Entities.Exceptions;

public class LimitExceededException : BadRequestException
{
    public LimitExceededException(string message) : base(message)
    {
    }
}