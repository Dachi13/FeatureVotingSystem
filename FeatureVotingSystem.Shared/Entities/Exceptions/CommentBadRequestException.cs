namespace FeatureVotingSystem.Shared.Entities.Exceptions;

public class CommentBadRequestException : BadRequestException
{
    public CommentBadRequestException(string message) : base(message)
    {
    }
}