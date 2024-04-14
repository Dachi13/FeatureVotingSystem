using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetRequestedFeaturesByStatus;

public class GetRequestedFeaturesByStatusRepository : IGetRequestedFeaturesByStatusRepository
{
    private readonly DapperContext _context;

    public GetRequestedFeaturesByStatusRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FeatureListByStatusResponse>> GetRequestedFeaturesByStatusAsync(int userId, TimeSpanModel timeSpanModel = null)
    {
        IEnumerable<FeatureListByStatusResponse> result = null;

        await using var connection = await _context.CreateConnectionAsync();
        {
            object parameters = new { UserId = userId, fromDate = timeSpanModel?.FromDate, toDate = timeSpanModel?.ToDate };

            result = await connection.QueryAsync<FeatureListByStatusResponse>("dbo.spGetRequestedFeaturesByStatus @UserId, @FromDate, @ToDate", parameters);
        }

        return result;
    }
}
