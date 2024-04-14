namespace FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;

public class CreateFeatureRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; private set; }
    public int ProductId { get; set; }
    public DateTime UploadDate { get; } = DateTime.Now;
    
    public void SetUserId(int userId) => UserId = userId;
}