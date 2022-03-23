using System;

namespace PJL.Utilities.Extensions
{
    public static class FloatingPointExtensions
    {
        private static float s_defaultSinglePrecision = .00001f;

        private static double s_defaultDoublePrecision = .00001;

        private static decimal s_defaultDecimalPrecision = .00001m;

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
    }
}
