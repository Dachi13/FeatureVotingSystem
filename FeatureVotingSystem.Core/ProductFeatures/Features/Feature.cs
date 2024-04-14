namespace FeatureVotingSystem.Core.ProductFeatures.Features;

public class Feature
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public int UpVote { get; set; }
    public int DownVote { get; set; }
    public int StatusId { get; set; }
    public int ProductId { get; set; }
    public string RejectionReason { get; set; }
    public DateTime UploadDate { get; set; }
}