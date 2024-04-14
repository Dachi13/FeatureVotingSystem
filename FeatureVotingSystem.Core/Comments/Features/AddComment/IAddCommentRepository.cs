namespace FeatureVotingSystem.Core.Comments.Features.AddComment;

public interface IAddCommentRepository
{
    public Task<int> AddAsync(AddCommentRequest request);
}