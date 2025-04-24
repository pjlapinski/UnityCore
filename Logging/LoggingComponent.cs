using UnityEngine;

namespace PJL.Logging {
public class LoggingComponent : MonoBehaviour {
    public void LogMessage(string message) => ContextLogger.Log(LogType.Log, "CORE", message);
    public void LogError(string message) => ContextLogger.Log(LogType.Error, "CORE", message);
    public void LogWarning(string message) => ContextLogger.Log(LogType.Warning, "CORE", message);
    public void LogAssertion(string message) => ContextLogger.Log(LogType.Assert, "CORE", message);
    public void LogException(string message) => ContextLogger.Log(LogType.Exception, "CORE", message);
}
}
