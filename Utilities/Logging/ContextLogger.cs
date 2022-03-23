using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace PJL.Utilities.Logging
{
    public enum Context { UI, Dialogues, Localization, AI, Core }

    public enum Severity { Message, Assertion, Warning, Error, }

    public static class ContextLogger
    {
        private static readonly Dictionary<Context, Color> ContextColors = new()
        {
            { Context.UI, new Color(0f, .6f, .9f) },
            { Context.Dialogues, new Color(.5f, .9f, .5f) },
            { Context.Localization, new Color(.9f, .9f, 0f) },
            { Context.AI, Color.Cyan },
            { Context.Core, Color.red }
        };

        private static readonly Dictionary<Severity, Color> SeverityColors = new()
        {
            { Severity.Assertion, new Color(.8f, .2f, .8f) },
            { Severity.Error, new Color(0.9f, 0.2f, 0.2f) },
            { Severity.Warning, new Color(.7f, .7f, 0.1f) },
        };

        private static readonly StringBuilder StringBuilder = new();

        private const string HtmlColorPrefix = "<color=#";
        private const string HtmlColorSuffix = "</color>";
        private static readonly string FormatInsertionColorHex = ColorUtility.ToHtmlStringRGB(new Color(0f, .6f, 0.9f));

        public static HashSet<Severity> ActiveLogLevels = new()
        {
            Severity.Message,
            Severity.Error,
            Severity.Assertion,
            Severity.Warning,
        };

        public static void Log(Severity severity, Context context, string message) =>
            LogFormat(severity, context, message);

        [StringFormatMethod("format")]
        public static void LogFormat(Severity severity, Context context, string format, params object[] insertions)
        {
            try
            {
                GenerateColoredText(ContextColors[context], $"[{context}]");
                StringBuilder.Append(' ');

                var coloredInsertions = insertions
                    .Select(insertion => $"{HtmlColorPrefix}{FormatInsertionColorHex}>{insertion}{HtmlColorSuffix}")
                    .OfType<object>()
                    .ToArray();
                switch (severity)
                {
                    case Severity.Message when ActiveLogLevels.Contains(Severity.Message):
                        StringBuilder.Append(format);
                        Debug.LogFormat(StringBuilder.ToString(), coloredInsertions);
                        break;
                    case Severity.Error when ActiveLogLevels.Contains(Severity.Error):
                        GenerateColoredText(SeverityColors[severity], format);
                        Debug.LogErrorFormat(StringBuilder.ToString(), coloredInsertions);
                        break;
                    case Severity.Assertion when ActiveLogLevels.Contains(Severity.Assertion):
                        GenerateColoredText(SeverityColors[severity], format);
                        Debug.LogAssertionFormat(StringBuilder.ToString(), coloredInsertions);
                        break;
                    case Severity.Warning when ActiveLogLevels.Contains(Severity.Warning):
                        GenerateColoredText(SeverityColors[severity], format);
                        Debug.LogWarningFormat(StringBuilder.ToString(), coloredInsertions);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
                }
            }
            finally
            {
                StringBuilder.Clear();
            }
        }

        private static void GenerateColoredText(string colorHex, string text)
        {
            StringBuilder.Append(HtmlColorPrefix);
            StringBuilder.Append(colorHex);
            StringBuilder.Append('>');
            StringBuilder.Append(text);
            StringBuilder.Append(HtmlColorSuffix);
        }

        private static void GenerateColoredText(Color color, string text) =>
            GenerateColoredText(ColorUtility.ToHtmlStringRGB(color), text);
    }
}
