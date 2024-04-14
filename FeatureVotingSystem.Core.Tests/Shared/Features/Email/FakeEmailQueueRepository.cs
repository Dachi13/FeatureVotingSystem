using FeatureVotingSystem.Shared.Features.AddEmailInQueue;
using FeatureVotingSystem.Shared.Features.GetQueuedEmails;

namespace FeatureVotingSystem.Core.Tests.Shared.Features.Email;

public class FakeEmailQueueRepository : IAddEmailInQueueRepository
{
    public Task<int> AddEmailInQueueAsync(int userId, int subjectId, string emailText)
    {
        return Task.FromResult(1);
    }

    public Task<IEnumerable<EmailQueue>> GetQueuedEmails()
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateEmailQueueStatus(int id)
    {
        throw new NotImplementedException();
    }
}