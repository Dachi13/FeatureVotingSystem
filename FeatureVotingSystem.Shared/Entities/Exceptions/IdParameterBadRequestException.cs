namespace FeatureVotingSystem.Shared.Entities.Exceptions;

public sealed class IdParameterBadRequestException : BadRequestException
{
    public IdParameterBadRequestException(int id) : base($"Request is sent with invalid id: {id}") {
    }
}