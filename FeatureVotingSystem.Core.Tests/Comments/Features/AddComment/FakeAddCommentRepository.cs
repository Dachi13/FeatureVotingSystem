using FeatureVotingSystem.Core.Comments.Features.AddComment;

namespace FeatureVotingSystem.Core.Tests.Comments.Features.AddComment;

public class FakeAddCommentRepository : IAddCommentRepository
{
    public Task<int> AddAsync(AddCommentRequest request)
    {
        return Task.FromResult(1);
    }
}