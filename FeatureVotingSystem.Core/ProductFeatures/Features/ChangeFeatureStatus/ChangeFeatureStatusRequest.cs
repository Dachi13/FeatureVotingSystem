namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;

public class ChangeFeatureStatusRequest
{
    public int UserId { get; private set; }
    public int FeatureId { get; set; }
    public int StatusId { get; set; }
    public string RejectionReason { get; set; }

    public void SetUserId(int userId) => UserId = userId;
}