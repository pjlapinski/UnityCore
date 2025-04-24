using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace PJL.Logging {
public static class ContextLogger {
    private const string HtmlColorPrefix = "<color=#";
    private const string HtmlColorSuffix = "</color>";

    private static readonly Dictionary<LogType, Color> LogTypeColors = new() {
        { LogType.Log, new Color(.3f, .7f, .4f) },
        { LogType.Assert, new Color(.8f, .2f, .8f) },
        { LogType.Error, new Color(0.9f, 0.2f, 0.2f) },
        { LogType.Warning, new Color(.7f, .7f, 0.1f) },
        { LogType.Exception, new Color(.5f, .2f, .5f) },
    };

    private static readonly StringBuilder StringBuilder = new();
    private static readonly string FormatInsertionColorHex = ColorUtility.ToHtmlStringRGB(new Color(0f, .6f, 0.9f));

    public static void TodoLog(object message) => Log(LogType.Warning, "TODO", message);

    [StringFormatMethod("format")]
    public static void TodoLogFormat(string format, params object[] insertions) => LogFormat(LogType.Warning, "TODO", format, insertions);

    public static void TestLog(object message) => TestLog("DEBUG", message);

    public static void TestLog(string context, object message) => TestLogFormat(context, message.ToString());

    [StringFormatMethod("format")]
    public static void TestLogFormat(string context, string format, params object[] insertions) {
#if UNITY_EDITOR
        LogFormat(LogType.Log, context, format, insertions);
#endif
    }

    public static void Log(LogType logType, string context, object message) { LogFormat(logType, context, message.ToString()); }

    [StringFormatMethod("format")]
    public static void LogFormat(LogType logType, string context, string format, params object[] insertions) {
        if (!Debug.unityLogger.IsLogTypeAllowed(logType)) return;
        try {
            var time = DateTime.Now.ToString("HH:mm:ss");
            GenerateColoredText(LogTypeColors[LogType.Log], $"[{time} -- {context}]");
            StringBuilder.Append(' ');

            var coloredInsertions = insertions
                .Select(
                    insertion =>
#if UNITY_EDITOR
                        SanitizeText($"{HtmlColorPrefix}{FormatInsertionColorHex}>{insertion}{HtmlColorSuffix}")
#else
                        SanitizeText(insertion.ToString())
#endif
                )
                .OfType<object>()
                .ToArray();
            if (logType == LogType.Log) {
                StringBuilder.Append(format);
            } else {
                GenerateColoredText(LogTypeColors[logType], format);
            }
            Debug.unityLogger.LogFormat(logType, StringBuilder.ToString(), coloredInsertions);
        } finally { StringBuilder.Clear(); }
    }

    private static void GenerateColoredText(string colorHex, string text) {
#if UNITY_EDITOR
        StringBuilder.Append(HtmlColorPrefix);
        StringBuilder.Append(colorHex);
        StringBuilder.Append('>');
        StringBuilder.Append(text);
        StringBuilder.Append(HtmlColorSuffix);
#else
        StringBuilder.Append(text);
#endif
    }

    private static void GenerateColoredText(Color color, string text) =>
        GenerateColoredText(ColorUtility.ToHtmlStringRGB(color), text);

    private static string SanitizeText(string text) => text.Replace("{", "{{").Replace("}", "}}");
}
}
