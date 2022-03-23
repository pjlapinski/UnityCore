using System;

namespace PJL.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static void SetFlag<T>(ref this T @enum, T flag, bool value) where T : struct, Enum
        {
            // Because the underlying type can be a byte, sbyte, short, ushort, int, uint, long or ulong
            // the method cannot be generic without the use of the 'dynamic' type
            var tType = Enum.GetUnderlyingType(typeof(T));
            dynamic enumValue = Convert.ChangeType(@enum, tType);
            dynamic flagValue = Convert.ChangeType(flag, tType);

            if (value)
            {
                @enum = (T)Enum.ToObject(typeof(T), enumValue | flagValue);
                return;
            }

            if (@enum.HasFlag(flag))
            {
                @enum = (T)Enum.ToObject(typeof(T), enumValue & ~flagValue);
            }
        }
    }
}
