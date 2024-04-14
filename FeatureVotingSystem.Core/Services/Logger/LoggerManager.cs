using Serilog;

namespace FeatureVotingSystem.Core.Services.Logger;

public class LoggerManager : ILoggerManager
{
    private static ILogger _logger;

    public LoggerManager(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void LogDebug(string message) => _logger.Debug(message);
    public void LogError(string message) => _logger.Error(message);
    public void LogInfo(string message, params object[] properties) => _logger.Information(message, properties);
    public void LogWarn(string message) => _logger.Warning(message);
}
