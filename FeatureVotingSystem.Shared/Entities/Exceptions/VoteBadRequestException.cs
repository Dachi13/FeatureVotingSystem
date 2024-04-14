namespace FeatureVotingSystem.Shared.Entities.Exceptions;

public class VoteBadRequestException : BadRequestException
{
    public VoteBadRequestException(string message) : base(message)
    {
    }
}