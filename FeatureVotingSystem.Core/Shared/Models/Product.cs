namespace FeatureVotingSystem.Core.Shared.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortDesc { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int IsDeleted { get; set; }
}
