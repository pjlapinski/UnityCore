using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace PJL.Data
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct Uuid : IFormattable, IComparable, IComparable<Uuid>, IEquatable<Uuid>
    {
        // Both those fields are only for Unity to know how to actually save the data on disk
        // They occupy the same space in memory as "Guid" and as such, any modifications to
        // the Guid variable, are also preserved in these ulongs. Newtonsoft serializes this class
        // as the string representation of the Guid
        [SerializeField, FieldOffset(0)] private ulong _0;
        [SerializeField, FieldOffset(8)] private ulong _1;
        [FieldOffset(0)] public Guid Guid;

        public bool Equals(Uuid other) => Guid.Equals(other.Guid);

        public int CompareTo(Uuid other) => Guid.CompareTo(other.Guid);

        public override bool Equals(object obj) => obj is Uuid other && Equals(other);

        public override int GetHashCode() => Guid.GetHashCode();

        public static implicit operator Guid(Uuid id) => id.Guid;

        public static implicit operator Uuid(Guid id) => new() { Guid = id };

        public static Uuid Empty => Guid.Empty;

        public static Uuid NewUuid() => Guid.NewGuid();

        public static Uuid Parse(ReadOnlySpan<char> s) => Guid.Parse(s);

        public static Uuid Parse(string s) => Guid.Parse(s);

        public static Uuid ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format) =>
            Guid.ParseExact(input, format);

        public static Uuid ParseExact(string input, string format) => Guid.ParseExact(input, format);

        public byte[] ToByteArray() => Guid.ToByteArray();

        public override string ToString() => Guid.ToString();

        public int CompareTo(object obj) => obj is Uuid other ? CompareTo(other) : -1;

        public string ToString(string format, IFormatProvider formatProvider) => Guid.ToString(format, formatProvider);

        public static bool TryParse(ReadOnlySpan<char> s, out Uuid result)
        {
            if (Guid.TryParse(s, out var id))
            {
                result = id;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParse(string s, out Uuid result)
        {
            if (Guid.TryParse(s, out var id))
            {
                result = id;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, out Uuid result)
        {
            if (Guid.TryParseExact(input, format, out var id))
            {
                result = id;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParseExact(string input, string format, out Uuid result)
        {
            if (Guid.TryParseExact(input, format, out var id))
            {
                result = id;
                return true;
            }

            result = default;
            return false;
        }

        public static bool operator ==(Uuid lhs, Uuid rhs) => lhs.Guid == rhs.Guid;
        public static bool operator !=(Uuid lhs, Uuid rhs) => lhs.Guid != rhs.Guid;
    }
}
