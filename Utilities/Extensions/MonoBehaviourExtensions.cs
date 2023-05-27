using PJL.Logging;
using UnityEngine;

namespace PJL.Utilities.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void DestroyObject(this MonoBehaviour mono) => Object.Destroy(mono.gameObject);

        public static void Log(this MonoBehaviour _, Severity severity, string context, object message) =>
            ContextLogger.Log(severity, context, message);

        public static void LogFmt(this MonoBehaviour _, Severity severity, string context, string format, params object[] insertions) =>
            ContextLogger.LogFormat(severity, context, format, insertions);
    }
}
