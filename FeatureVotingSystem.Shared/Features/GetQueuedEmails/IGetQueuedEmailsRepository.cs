namespace FeatureVotingSystem.Shared.Features.GetQueuedEmails;

public interface IGetQueuedEmailsRepository
{
    Task<IEnumerable<EmailQueue>> GetQueuedEmailsAsync();

}
