namespace FeatureVotingSystem.Worker.EmailSenderService;

public interface IEmailSender
{
    public Task SendEmailAsync(string subject, string toEmail, string username, string message);
}
