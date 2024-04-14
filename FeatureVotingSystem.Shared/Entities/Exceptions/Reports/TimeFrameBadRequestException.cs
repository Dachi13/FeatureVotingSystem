namespace FeatureVotingSystem.Shared.Entities.Exceptions.Reports;

public class TimeFrameBadRequestException : BadRequestException
{
    public TimeFrameBadRequestException(string reportTimeFrame) : base($"You entered invalid time frame: {reportTimeFrame}. Please, enter 30 - for month or 7 - for week")
    {
    }
}
