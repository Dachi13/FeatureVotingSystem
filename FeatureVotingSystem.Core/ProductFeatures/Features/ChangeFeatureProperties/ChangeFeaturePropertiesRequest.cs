namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;

public class ChangeFeaturePropertiesRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; private set; }

    public void SetUserId(int userId) => UserId = userId;
}