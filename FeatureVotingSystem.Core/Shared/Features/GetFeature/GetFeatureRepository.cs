using Dapper;
using FeatureVotingSystem.Core.ProductFeatures.Features;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Shared.Features.GetFeature;

public class GetFeatureRepository : IGetFeatureRepository
{
    private readonly DapperContext _context;

    public GetFeatureRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Feature?> GetByIdAsync(int featureId)
    {
        var query = "SELECT * FROM Features WHERE Id = @featureId";

        await using var connection = await _context.CreateConnectionAsync();
        var feature = await connection.QueryFirstOrDefaultAsync<Feature>(query, new { featureId });

        return feature;
    }
}