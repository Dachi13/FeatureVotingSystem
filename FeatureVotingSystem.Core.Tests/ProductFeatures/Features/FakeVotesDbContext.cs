using FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features;

public static class FakeVotesDbContext
{
    public static async Task<List<Vote>> GetListOfDummyVotes()
    {
        var votes = new List<Vote>();
        var random = new Random();

        for (var i = 0; i < 20; i++)
        {
            votes.Add(new()
            {
                UserId = i,
                ProductId = i,
                FeatureId = i,
                VoteValue = 1,
                VotedAt = DateTime.Now.AddDays(-random.Next(10))
            });
        }

        return await Task.FromResult(votes);
    }
}