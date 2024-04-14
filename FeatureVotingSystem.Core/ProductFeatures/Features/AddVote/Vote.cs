namespace FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;

public class Vote
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int FeatureId { get; set; }
    public int VoteValue { get; set; }
    public DateTime VotedAt { get; set; }
    public int IsDeleted { get; set; }
    
    public bool Equals(VoteRequest vote)
    {
        return FeatureId == vote.FeatureId;
    }

    public override string ToString()
    {
        return $"UserId: {UserId}, ProductId: {ProductId}, FeatureId: {FeatureId}, VoteValue: {VoteValue}, VotedAt: {VotedAt}";
    }
}