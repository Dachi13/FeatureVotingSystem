using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.DeleteFeature;

public class DeleteFeatureRepository : IDeleteFeatureRepository
{
    private readonly DapperContext _context;

    public DeleteFeatureRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<int> ByIdAsync(int featureId)
    {
        await using var connection = await _context.CreateConnectionAsync();

        int affectedRows = await connection.ExecuteAsync("dbo.SoftDeleteFeatures @FeatureId", new { FeatureId = featureId });

        return affectedRows;
    }
}