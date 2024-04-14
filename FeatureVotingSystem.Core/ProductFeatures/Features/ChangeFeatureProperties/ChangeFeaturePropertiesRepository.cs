using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;

public class ChangeFeaturePropertiesRepository : IChangeFeaturePropertiesRepository
{
    private readonly DapperContext _context;

    public ChangeFeaturePropertiesRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<int> ChangeAsync(ChangeFeaturePropertiesRequest featurePropertiesRequest)
    {
        var query =
            "UPDATE Features SET Name = @Name, [Description] = @Description WHERE Id = @Id";
        
        await using var connection = await _context.CreateConnectionAsync();
        
        var rowsAffected = await connection.ExecuteAsync(query, featurePropertiesRequest);

        return rowsAffected;
    }
}