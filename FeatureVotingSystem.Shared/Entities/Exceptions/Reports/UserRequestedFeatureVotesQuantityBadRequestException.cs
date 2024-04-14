namespace FeatureVotingSystem.Shared.Entities.Exceptions.Reports;

public class UserRequestedFeatureVotesQuantityBadRequestException : BadRequestException
{
    public UserRequestedFeatureVotesQuantityBadRequestException() : base("UserRequestedFeatureVotesQuantityRequest can't be null")
    {
    }
}
