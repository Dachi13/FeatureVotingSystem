namespace FeatureVotingSystem.Shared.Features.UpdateEmailQueueStatus;

public interface IUpdateEmailQueueStatusRepository
{
    Task<int> UpdateEmailQueueStatusAsync(int id);
}
