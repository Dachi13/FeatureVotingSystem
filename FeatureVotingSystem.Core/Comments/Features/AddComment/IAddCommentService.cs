namespace FeatureVotingSystem.Core.Comments.Features.AddComment;

public interface IAddCommentService
{
    Task<int> AddCommentAsync(AddCommentRequest request);
}