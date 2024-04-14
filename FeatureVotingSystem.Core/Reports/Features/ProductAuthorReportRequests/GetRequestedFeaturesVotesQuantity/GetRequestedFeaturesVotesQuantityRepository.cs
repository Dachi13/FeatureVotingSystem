using Dapper;
using FeatureVotingSystem.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesVotesQuantity;

public class GetRequestedFeaturesVotesQuantityRepository : IGetRequestedFeaturesVotesQuantityRepository
{
    private readonly DapperContext _context;
    private IMemoryCache _cache;

    public GetRequestedFeaturesVotesQuantityRepository(DapperContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<FeatureVotesQuantityResponse>> GetRequestedFeaturesVotesQuantityAsync(int productId, TimeSpanModel timeSpanModel = null)
    {
        var cacheKey = $"{productId}_{timeSpanModel?.FromDate}_{timeSpanModel?.ToDate}";

        if (_cache.TryGetValue(cacheKey, out IEnumerable<FeatureVotesQuantityResponse> cachedResult))
        {
            return cachedResult;
        }

        await using var connection = await _context.CreateConnectionAsync();
        {
            object parameters = new { productId, fromDate = timeSpanModel?.FromDate, toDate = timeSpanModel?.ToDate };

            var resultFromDatabase = await connection.QueryAsync<FeatureVotesQuantityResponse>("dbo.spGetRequestedFeaturesVotesQuantity @ProductId, @FromDate, @ToDate", parameters);

            // Store the result in the cache
            _cache.Set(cacheKey, resultFromDatabase, TimeSpan.FromMinutes(5)); // Adjust expiration time as needed
            return resultFromDatabase;
        }
    }
}
