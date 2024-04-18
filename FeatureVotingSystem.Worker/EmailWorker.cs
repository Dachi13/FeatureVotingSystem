using FeatureVotingSystem.Core.Services.Logger;
using FeatureVotingSystem.Shared.Features.GetQueuedEmails;
using FeatureVotingSystem.Shared.Features.UpdateEmailQueueStatus;
using FeatureVotingSystem.Worker.EmailSenderService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FeatureVotingSystem.Worker;

public class EmailWorker : BackgroundService
{
    private readonly WorkerConfig _workerConfig;
    private readonly IEmailSender _emailSender;
    private readonly ILoggerManager _loggerManager;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public EmailWorker(
        IOptions<Config> config,
        IEmailSender emailSender,
        ILoggerManager loggerManager,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _workerConfig = config.Value.WorkerConfig;
        _emailSender = emailSender;
        _loggerManager = loggerManager;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var getQueuedEmailsRepository = scope.ServiceProvider.GetRequiredService<IGetQueuedEmailsRepository>();
                var updateEmailQueueStatusRepository =
                    scope.ServiceProvider.GetRequiredService<IUpdateEmailQueueStatusRepository>();
                var queue = await getQueuedEmailsRepository.GetQueuedEmailsAsync();

                foreach (var item in queue)
                    try
                    {
                        await _emailSender.SendEmailAsync(item.SubjectName, item.Email, item.UserName, item.EmailText);
                        await updateEmailQueueStatusRepository.UpdateEmailQueueStatusAsync(item.Id);
                    }
                    catch (Exception e)
                    {
                        _loggerManager.LogError(e.Message);
                    }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
            }

            await Task.Delay(_workerConfig.DelayInSeconds, stoppingToken);
        }
    }
}