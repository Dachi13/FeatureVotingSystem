namespace FeatureVotingSystem.Shared.Features.AddEmailInQueue;

public interface IAddEmailInQueueRepository
{
    Task<int> AddEmailInQueueAsync(int userId, int subjectId, string emailText);
}
