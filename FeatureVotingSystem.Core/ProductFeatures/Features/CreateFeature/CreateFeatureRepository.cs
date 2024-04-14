using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;

public class CreateFeatureRepository : ICreateFeatureRepository
{
    private readonly DapperContext _context;

    public CreateFeatureRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<int> CreateAsync(CreateFeatureRequest request)
    {
        var query =
            "INSERT INTO Features(Name,[Description],UserId, ProductId, UploadDate) VALUES (@Name, @Description, @UserId, @ProductId, @UploadDate)";

        await using var connection = await _context.CreateConnectionAsync();

        var rowsAffected = await connection.ExecuteAsync(query, request);

        return rowsAffected;
    }
    
    public async Task<int> GetTotalFeaturesUploadedByUserSinceDateAsync(int userId, int productId, DateTime dateLimit)
    {
        var query =
            "SELECT COUNT(*) FROM Features WHERE UserId = @userId AND ProductId = @productId AND UploadDate > @dateLimit AND StatusId != 3";
        await using var connection = await _context.CreateConnectionAsync();
        var totalFeaturesOnProductByUser =
            await connection.ExecuteScalarAsync<int>(query, new { userId, productId, dateLimit });

        return totalFeaturesOnProductByUser;
    }
}