using FeatureVotingSystem.Shared.Entities.Exceptions.Reports;

namespace FeatureVotingSystem.Core.Reports;

public static class TimeSpanHelper
{
    public static TimeSpanModel GetTimeSpan(ReportTimeFrame timeFrame)
    {
        TimeSpanModel timeSpanModel = timeFrame switch
        {
            ReportTimeFrame.Month => new TimeSpanModel() { FromDate = DateTime.Now.AddDays(-(int)ReportTimeFrame.Month), ToDate = DateTime.Now },
            ReportTimeFrame.Week => new TimeSpanModel() { FromDate = DateTime.Now.AddDays(-(int)ReportTimeFrame.Week), ToDate = DateTime.Now },
            _ => throw new TimeFrameBadRequestException("Unhandled report time frame")
        };

        return timeSpanModel;
    }
}
