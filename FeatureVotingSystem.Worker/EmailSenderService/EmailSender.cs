using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FeatureVotingSystem.Worker.EmailSenderService;

public class EmailSender : IEmailSender
{
    private readonly EmailConfig _emailConfig;
    private static readonly string CachedHtml = GetHtmlFromFile();

    public EmailSender(IOptions<Config> emailConfig)
    {
        _emailConfig = emailConfig.Value.EmailConfig;
    }

    public async Task SendEmailAsync(string subject, string toEmail, string username, string message)
    {
        var apiKey = _emailConfig.ApiKey;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_emailConfig.FromEmail, "FeatureVotingSystem");
        var to = new EmailAddress(toEmail, username);
        var plainTextContent = message;
        var htmlContent = CachedHtml.Replace("[username]", username).Replace("[message]", message);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        await client.SendEmailAsync(msg);
    }

    private static string GetHtmlFromFile()
    {
        var path = GetPathToEmailHtml();
        using var reader = new StreamReader(path);
        var extractedHtml = reader.ReadToEnd();

        return extractedHtml;
    }

    private static string GetPathToEmailHtml()
    {
        var baseDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var parentDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory())!.Name;
        var workerPath = string.Concat(parentDirectoryPath, ".Worker");
        var path = Path.Combine(baseDirectoryPath, workerPath, "EmailContext", "index.html");

        return path;
    }
}