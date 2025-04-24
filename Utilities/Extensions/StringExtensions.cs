using System;
using System.Text;
using UnityEngine.Localization;

namespace PJL.Utilities.Extensions {
public static class StringExtensions {

    public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

    public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

    public static string PascalOrCamelToSnakeCase(this ReadOnlySpan<char> input) {
        var builder = new StringBuilder();

        foreach (var ch in input) {
            if (char.IsUpper(ch)) {
                if (builder.Length > 0) {
                    builder.Append("_");
                }
                builder.Append(char.ToLower(ch));
            } else {
                builder.Append(ch);
            }
        }
        return builder.ToString();
    }

    public static string PascalOrCamelToScreamCase(this ReadOnlySpan<char> input) {
        var builder = new StringBuilder();

        foreach (var ch in input) {
            if (char.IsUpper(ch)) {
                if (builder.Length > 0) {
                    builder.Append("_");
                }
            }
            builder.Append(char.ToUpper(ch));
        }
        return builder.ToString();
    }

    public static LocalizedString ToLocalizedString(this string input) {
        var parts = input.Split('/');
        return new LocalizedString(parts[0], parts[1]);
    }
}
}
