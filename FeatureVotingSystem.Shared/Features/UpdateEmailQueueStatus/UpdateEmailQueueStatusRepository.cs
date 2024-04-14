using Dapper;

namespace FeatureVotingSystem.Shared.Features.UpdateEmailQueueStatus;

public class UpdateEmailQueueStatusRepository : IUpdateEmailQueueStatusRepository
{
    private readonly DapperContext _context;

    public UpdateEmailQueueStatusRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> UpdateEmailQueueStatusAsync(int id)
    {
        await using var connection = await _context.CreateConnectionAsync();
        int affectedRows =
            await connection.ExecuteAsync("dbo.spMarkEmailAsSent @EmailQueueId", new { EmailQueueId = id });

        return affectedRows;
    }
}