namespace FeatureVotingSystem.Shared.Entities.Exceptions.Reports;

public class FeatureListByStatusBadRequestException : BadRequestException
{
    public FeatureListByStatusBadRequestException() : base("FeatureListByStatusRequest can't be null")
    {
    }
}
