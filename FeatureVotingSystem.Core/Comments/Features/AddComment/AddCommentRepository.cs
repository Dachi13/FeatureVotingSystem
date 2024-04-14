using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Comments.Features.AddComment;

public class AddCommentRepository : IAddCommentRepository
{
    private readonly DapperContext _context;

    public AddCommentRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(AddCommentRequest request)
    {
        var query = "INSERT INTO Comments VALUES (@CreatedAt, @UserId, @FeatureId, @Text, 0)";
        await using var connection = await _context.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, request);

        return affectedRows;
    }
}