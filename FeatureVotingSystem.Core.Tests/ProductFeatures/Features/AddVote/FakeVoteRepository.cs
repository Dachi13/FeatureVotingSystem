using FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.AddVote;

public class FakeVoteRepository : IVoteRepository
{
    public async Task<int> AddVoteAsync(VoteRequest voteRequest)
    {
        return await Task.FromResult(1);
    }

    public Task<int> UpdateVoteAsync(VoteRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Vote?> GetVoteOnFeatureByUserAsync(int userId, int featureId)
    {
        var votes = await FakeVotesDbContext.GetListOfDummyVotes();
        var vote = votes.FirstOrDefault(v => v.UserId == userId && v.FeatureId == featureId);

        return vote;
    }

    public async Task<int> TotalUserVotesOnProductAsync(int userId, int productId, DateTime dateLimit)
    {
        return userId % 2 == 0
            ? await Task.FromResult(2)
            : await Task.FromResult(4);
    }
}