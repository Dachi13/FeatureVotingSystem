using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetUserRequestedFeaturesVotesQuantity;

public class GetUserRequestedFeaturesVotesQuantityRepository : IGetUserRequestedFeaturesVotesQuantityRepository
{
    private readonly DapperContext _context;

    public GetUserRequestedFeaturesVotesQuantityRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserRequestedFeatureVotesQuantityResponse>> GetUserRequestedFeaturesVotesQuantity(int userId, TimeSpanModel timeSpanModel = null)
    {
        IEnumerable<UserRequestedFeatureVotesQuantityResponse> result = null;

        await using var connection = await _context.CreateConnectionAsync();
        {
            object parameters = new { UserId = userId, fromDate = timeSpanModel?.FromDate, toDate = timeSpanModel?.ToDate };

            result = await connection.QueryAsync<UserRequestedFeatureVotesQuantityResponse>("dbo.spGetUserRequestedFeaturesVotesQuantity @UserId, @FromDate, @ToDate", parameters);
        }

        return result;
    }
}
