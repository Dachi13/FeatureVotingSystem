using Dapper;

namespace FeatureVotingSystem.Shared.Features.AddEmailInQueue;

public class AddEmailInQueueRepository : IAddEmailInQueueRepository
{
    private readonly DapperContext _context;

    public AddEmailInQueueRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> AddEmailInQueueAsync(int userId, int subjectId, string emailText)
    {
        var insertQuery =
            "INSERT INTO EmailQueue(UserId, EmailSubjectId, EmailText) values (@userId, @subjectId, @emailText)";

        await using var connection = await _context.CreateConnectionAsync();
        int affectedRows = await connection.ExecuteAsync(insertQuery, new { userId, subjectId, emailText });

        return affectedRows;
    }
}