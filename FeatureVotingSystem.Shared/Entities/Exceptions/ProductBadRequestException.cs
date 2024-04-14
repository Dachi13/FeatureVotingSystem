namespace FeatureVotingSystem.Shared.Entities.Exceptions;

public sealed class ProductBadRequestException : BadRequestException
{
    public ProductBadRequestException(string message) : base(message) { }
}
