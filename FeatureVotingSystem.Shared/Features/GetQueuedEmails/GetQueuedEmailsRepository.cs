using Dapper;

namespace FeatureVotingSystem.Shared.Features.GetQueuedEmails;

public class GetQueuedEmailsRepository : IGetQueuedEmailsRepository
{
    private readonly DapperContext _context;

    public GetQueuedEmailsRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmailQueue>> GetQueuedEmailsAsync()
    {
        await using var connection = await _context.CreateConnectionAsync();
        var result = await connection.QueryAsync<EmailQueue>("dbo.spGetEmailQueue");

        return result;
    }
}