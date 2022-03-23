using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace PJL.Utilities.Extensions
{
    public static class EnumExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFlag<T>(ref this T @enum, T flag, bool value) where T : struct, Enum
        {
            var enumSize = UnsafeUtility.SizeOf<T>();
            if (enumSize == 1)
            {
                var newValue = (byte)(value
                    ? UnsafeUtility.As<T, byte>(ref @enum) | UnsafeUtility.As<T, byte>(ref flag)
                    : UnsafeUtility.As<T, byte>(ref @enum) & ~UnsafeUtility.As<T, byte>(ref flag));
                @enum = UnsafeUtility.As<byte, T>(ref newValue);
            }
            else if (enumSize == 2)
            {
                var newValue = (short)(value
                    ? UnsafeUtility.As<T, short>(ref @enum) | UnsafeUtility.As<T, short>(ref flag)
                    : UnsafeUtility.As<T, short>(ref @enum) & ~UnsafeUtility.As<T, short>(ref flag));
                @enum = UnsafeUtility.As<short, T>(ref newValue);
            }
            else if (enumSize == 4)
            {
                var newValue = value
                    ? UnsafeUtility.As<T, uint>(ref @enum) | UnsafeUtility.As<T, uint>(ref flag)
                    : UnsafeUtility.As<T, uint>(ref @enum) & ~UnsafeUtility.As<T, uint>(ref flag);
                @enum = UnsafeUtility.As<uint, T>(ref newValue);
            }
            else
            {
                var newValue = value
                    ? UnsafeUtility.As<T, ulong>(ref @enum) | UnsafeUtility.As<T, ulong>(ref flag)
                    : UnsafeUtility.As<T, ulong>(ref @enum) & ~UnsafeUtility.As<T, ulong>(ref flag);
                @enum = UnsafeUtility.As<ulong, T>(ref newValue);
            }
        }
    }
}