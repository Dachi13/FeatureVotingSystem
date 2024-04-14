namespace FeatureVotingSystem.Core.Services.Logger;

public interface ILoggerManager
{
    void LogInfo(string message, params object[] properties);
    void LogWarn(string message);
    void LogDebug(string message);
    void LogError(string message);
}
