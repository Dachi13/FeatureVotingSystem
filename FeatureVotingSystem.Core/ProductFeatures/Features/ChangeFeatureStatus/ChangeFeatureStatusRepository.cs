using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;

public class ChangeFeatureStatusRepository : IChangeFeatureStatusRepository
{
    private readonly DapperContext _context;

    public ChangeFeatureStatusRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> ChangeAsync(ChangeFeatureStatusRequest request)
    {
        var query =
            "UPDATE Features SET StatusId = @StatusId, RejectionReason = @RejectionReason WHERE Id = @FeatureId";
        await using var connection = await _context.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(query, request);

        return rowsAffected;
    }
}