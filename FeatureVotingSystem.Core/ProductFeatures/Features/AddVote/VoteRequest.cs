namespace FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;

public class VoteRequest
{
    public int UserId { get; private set; }
    public int FeatureId { get; set; }
    public int ProductId { get; private set; }
    public int VoteValue { get; private set; }
    public DateTime VotedAt { get; } = DateTime.Now;
    
    public void SetUserId(int userId) => UserId = userId;
    public void SetVoteValue(int voteValue) => VoteValue = voteValue;
    public void SetProductId(int productId) => ProductId = productId;
}