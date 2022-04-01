using UnityEngine;

namespace PJL.Logging
{
    public class LoggingComponent : MonoBehaviour
    {
        public void LogMessage(string message) => ContextLogger.Log(Severity.Message, Context.Core, message);
        public void LogError(string message) => ContextLogger.Log(Severity.Error, Context.Core, message);
        public void LogWarning(string message) => ContextLogger.Log(Severity.Warning, Context.Core, message);
        public void LogAssertion(string message) => ContextLogger.Log(Severity.Assertion, Context.Core, message);
    }
}
