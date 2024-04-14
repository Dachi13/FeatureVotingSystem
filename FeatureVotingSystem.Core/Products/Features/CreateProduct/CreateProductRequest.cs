namespace FeatureVotingSystem.Core.Products.Features.CreateProduct;

public class CreateProductRequest
{
    public string Name { get; set; }
    public string ShortDesc { get; set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; } = DateTime.Now;

    public void SetUserId(int userId) => UserId = userId;
}
