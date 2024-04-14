namespace FeatureVotingSystem.Shared.Entities.Exceptions;

public class FeatureBadRequestException : BadRequestException
{
    public FeatureBadRequestException(string message) : base(message)
    {
    }
}