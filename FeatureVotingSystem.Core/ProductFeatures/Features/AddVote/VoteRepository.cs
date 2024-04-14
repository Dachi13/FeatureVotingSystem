using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;

public class VoteRepository : IVoteRepository
{
    private readonly DapperContext _context;

    public VoteRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> AddVoteAsync(VoteRequest voteRequest)
    {
        var query = "INSERT INTO Votes VALUES(@UserId, @FeatureId, @ProductId, @VoteValue, @VotedAt, 0)";

        await using var connection = await _context.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(query, voteRequest);

        return rowsAffected;
    }

    public async Task<int> UpdateVoteAsync(VoteRequest request)
    {
        var query =
            "UPDATE Votes SET VoteValue = @VoteValue, VotedAt = @VotedAt WHERE FeatureId = @FeatureId AND UserId = @UserId AND IsDeleted = 0";
        await using var connection = await _context.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(query, request);

        return rowsAffected;
    }

    public async Task<Vote?> GetVoteOnFeatureByUserAsync(int userId, int featureId)
    {
        var query = "SELECT * FROM Votes WHERE UserId = @userId AND FeatureId = @featureId AND IsDeleted = 0";
        await using var connection = await _context.CreateConnectionAsync();
        var vote = await connection.QueryFirstOrDefaultAsync<Vote>(query, new { userId, featureId });

        return vote;
    }

    public async Task<int> TotalUserVotesOnProductAsync(int userId, int productId, DateTime dateLimit)
    {
        var query =
            "SELECT COUNT(*) FROM Votes WHERE UserId = @userId AND ProductId = @productId AND VotedAt > @dateLimit";
        await using var connection = await _context.CreateConnectionAsync();
        var totalUserVotesOnProduct =
            await connection.ExecuteScalarAsync<int>(query, new { userId, productId, dateLimit });

        return totalUserVotesOnProduct;
    }
}