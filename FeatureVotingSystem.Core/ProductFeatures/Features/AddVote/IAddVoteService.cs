namespace FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;

public interface IAddVoteService
{
    Task<int> UpVoteAsync(string userId, int featureId);
    Task<int> DownVoteAsync(string userId, int featureId);
}