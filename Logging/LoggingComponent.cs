using UnityEngine;

namespace PJL.Logging
{
    public class LoggingComponent : MonoBehaviour
    {
        public void LogMessage(string message) => ContextLogger.Log(Severity.Message, "CORE", message);
        public void LogError(string message) => ContextLogger.Log(Severity.Error, "CORE", message);
        public void LogWarning(string message) => ContextLogger.Log(Severity.Warning, "CORE", message);
        public void LogAssertion(string message) => ContextLogger.Log(Severity.Assertion, "CORE", message);
    }
}