﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace PJL.Logging {
public enum Severity { Message, Assertion, Warning, Error, }

public static class ContextLogger {
    private const string HtmlColorPrefix = "<color=#";
    private const string HtmlColorSuffix = "</color>";

    private static readonly Dictionary<Severity, Color> SeverityColors = new() {
        { Severity.Message, new Color(.3f, .7f, .4f) },
        { Severity.Assertion, new Color(.8f, .2f, .8f) },
        { Severity.Error, new Color(0.9f, 0.2f, 0.2f) },
        { Severity.Warning, new Color(.7f, .7f, 0.1f) },
    };

    private static readonly StringBuilder StringBuilder = new();
    private static readonly string FormatInsertionColorHex = ColorUtility.ToHtmlStringRGB(new Color(0f, .6f, 0.9f));

    public static HashSet<Severity> ActiveLogLevels = new() {
        Severity.Message,
        Severity.Error,
        Severity.Assertion,
        Severity.Warning,
    };

    public static void TestLog(object message) { TestLog("DEBUG", message); }

    public static void TestLog(string context, object message) { TestLogFormat(context, message.ToString()); }

    [StringFormatMethod("format")]
    public static void TestLogFormat(string context, string format, params object[] insertions) {
#if UNITY_EDITOR
        try {
            var time = DateTime.Now.ToString("HH:mm:ss");
            GenerateColoredText(SeverityColors[Severity.Message], $"[{time} -- {context}]");
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
            StringBuilder.Append(format);
            Debug.LogFormat(StringBuilder.ToString(), coloredInsertions);
        } finally { StringBuilder.Clear(); }
#endif
    }

    public static void Log(Severity severity, string context, object message) { LogFormat(severity, context, message.ToString()); }

    [StringFormatMethod("format")]
    public static void LogFormat(Severity severity, string context, string format, params object[] insertions) {
        if (!ActiveLogLevels.Contains(severity)) return;
        try {
            var time = DateTime.Now.ToString("HH:mm:ss");
            GenerateColoredText(SeverityColors[Severity.Message], $"[{time} -- {context}]");
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
            switch (severity) {
                case Severity.Message:
                    StringBuilder.Append(format);
                    Debug.LogFormat(StringBuilder.ToString(), coloredInsertions);
                    break;
                case Severity.Error:
                    GenerateColoredText(SeverityColors[severity], format);
                    Debug.LogErrorFormat(StringBuilder.ToString(), coloredInsertions);
                    break;
                case Severity.Assertion:
                    GenerateColoredText(SeverityColors[severity], format);
                    Debug.LogAssertionFormat(StringBuilder.ToString(), coloredInsertions);
                    break;
                case Severity.Warning:
                    GenerateColoredText(SeverityColors[severity], format);
                    Debug.LogWarningFormat(StringBuilder.ToString(), coloredInsertions);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
            }
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
