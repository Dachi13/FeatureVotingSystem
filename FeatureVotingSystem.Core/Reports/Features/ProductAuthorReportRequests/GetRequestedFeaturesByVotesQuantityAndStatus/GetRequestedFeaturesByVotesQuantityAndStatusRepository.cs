using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesByVotesQuantityAndStatus;

public class GetRequestedFeaturesByVotesQuantityAndStatusRepository : IGetRequestedFeaturesByVotesQuantityAndStatusRepository
{
    private readonly DapperContext _context;

    public GetRequestedFeaturesByVotesQuantityAndStatusRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FeatureListByVotesQuantityAndStatusResponse>> GetRequestedFeaturesByVotesQuantityAndStatusAsync(int productId, TimeSpanModel timeSpanModel = null)
    {
        IEnumerable<FeatureListByVotesQuantityAndStatusResponse> result = null;

        await using var connection = await _context.CreateConnectionAsync();
        {
            object parameters = new { productId, fromDate = timeSpanModel?.FromDate, toDate = timeSpanModel?.ToDate };

            result = await connection.QueryAsync<FeatureListByVotesQuantityAndStatusResponse>("dbo.spGetRequestedFeaturesByVotesQuantityAndStatus @ProductId, @FromDate, @ToDate", parameters);
        }

        return result;
    }
}
