using System;

namespace PJL.Utilities.Extensions {
public static class FloatingPointExtensions {
    private static readonly float s_defaultSinglePrecision = .00001f;
    private static readonly double s_defaultDoublePrecision = .00001;
    private static readonly decimal s_defaultDecimalPrecision = .00001m;

    public static bool AlmostEquals(this float first, float other, float precision) =>
        Math.Abs(first - other) <= precision;

    public static bool AlmostEquals(this float first, float other) =>
        AlmostEquals(first, other, s_defaultSinglePrecision);

    public static bool AlmostEquals(this double first, double other, double precision) =>
        Math.Abs(first - other) <= precision;

    public static bool AlmostEquals(this double first, double other) =>
        AlmostEquals(first, other, s_defaultDoublePrecision);

    public static bool AlmostEquals(this decimal first, decimal other, decimal precision) =>
        Math.Abs(first - other) <= precision;

    public static bool AlmostEquals(this decimal first, decimal other) =>
        AlmostEquals(first, other, s_defaultDecimalPrecision);

    public static float Remap(this float value, float minStart, float maxStart, float minEnd, float maxEnd) =>
        minEnd + (maxEnd - minEnd) * (value - minStart) / (maxStart - minStart);

    public static double Remap(this double value, double minStart, double maxStart, double minEnd, double maxEnd) =>
        minEnd + (maxEnd - minEnd) * (value - minStart) / (maxStart - minStart);

    public static decimal Remap(this decimal value, decimal minStart, decimal maxStart, decimal minEnd, decimal maxEnd) =>
        minEnd + (maxEnd - minEnd) * (value - minStart) / (maxStart - minStart);

    public static float Clamp(this float value, float min, float max) =>
        value < min ? min : value > max ? max : value;

    public static double Clamp(this double value, double min, double max) =>
        value < min ? min : value > max ? max : value;

    public static decimal Clamp(this decimal value, decimal min, decimal max) =>
        value < min ? min : value > max ? max : value;
}
}
