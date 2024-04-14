namespace FeatureVotingSystem.Core.Comments.Features.AddComment;

public class AddCommentRequest
{
    public int UserId { get; private set; }
    public int FeatureId { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public string Text { get; set; }
    
    public void SetUserId(int userId) => UserId = userId;
}