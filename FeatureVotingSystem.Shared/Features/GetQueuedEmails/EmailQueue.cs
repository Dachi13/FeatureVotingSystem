namespace FeatureVotingSystem.Shared.Features.GetQueuedEmails;

public class EmailQueue
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email{ get; set; }
    public string SubjectName { get; set; }
    public string EmailText { get; set; }
}
