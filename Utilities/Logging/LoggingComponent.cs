using UnityEngine;

namespace PJL.Utilities.Logging
{
    public class LoggingComponent : MonoBehaviour
    {
        public void LogMessage(string message) => Logger.Log(Severity.Message, Context.Core, message);
        public void LogError(string message) => Logger.Log(Severity.Error, Context.Core, message);
        public void LogWarning(string message) => Logger.Log(Severity.Warning, Context.Core, message);
        public void LogAssertion(string message) => Logger.Log(Severity.Assertion, Context.Core, message);
    }
}
