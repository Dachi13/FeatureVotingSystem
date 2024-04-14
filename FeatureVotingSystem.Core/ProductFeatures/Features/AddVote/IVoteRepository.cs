namespace FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;

public interface IVoteRepository
{
    Task<int> AddVoteAsync(VoteRequest voteRequest);
    Task<int> UpdateVoteAsync(VoteRequest request);
    Task<Vote?> GetVoteOnFeatureByUserAsync(int userId, int featureId);
    Task<int> TotalUserVotesOnProductAsync(int userId, int productId, DateTime dateLimit);
}