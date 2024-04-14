using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesQuantity;

public class GetRequestedFeaturesQuantityRepository : IGetRequestedFeaturesQuantityRepository
{
    private readonly DapperContext _context;

    public GetRequestedFeaturesQuantityRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> GetRequestedFeaturesQuantityAsync(int productId, TimeSpanModel? timeSpanModel = null)
    {
        int result = 0;

        await using var connection = await _context.CreateConnectionAsync();
        {
            object parameters = new { productId, fromDate = timeSpanModel?.FromDate, toDate = timeSpanModel?.ToDate };

            result = await connection.QueryFirstOrDefaultAsync<int>("dbo.spGetRequestedFeaturesQuantity @ProductId, @FromDate, @ToDate", parameters);
        }

        return result;
    }
}
