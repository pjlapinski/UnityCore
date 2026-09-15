using System;
using UnityEngine;

namespace PJL.Data
{
    [Serializable]
    public struct MinMaxRange
    {
        [field: SerializeField] public float Min { get; set; }
        [field: SerializeField] public float Max { get; set; }

        public MinMaxRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public MinMaxRange(Vector2 vec) : this(vec.x, vec.y) { }

        public void Deconstruct(out float min, out float max)
        {
            min = Min;
            max = Max;
        }

        public static implicit operator Vector2(MinMaxRange r) => new(r.Min, r.Max);
        public static implicit operator MinMaxRange(Vector2 vec) => new(vec.x, vec.y);
    }

    [Serializable]
    public struct MinMaxRangeInt
    {
        [field: SerializeField] public int Min { get; set; }
        [field: SerializeField] public int Max { get; set; }

        public MinMaxRangeInt(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public MinMaxRangeInt(Vector2Int vec) : this(vec.x, vec.y) { }
        public MinMaxRangeInt(Vector2 vec) : this((int)vec.x, (int)vec.y) { }

        public void Deconstruct(out int min, out int max)
        {
            min = Min;
            max = Max;
        }

        public static implicit operator Vector2(MinMaxRangeInt r) => new(r.Min, r.Max);
        public static implicit operator Vector2Int(MinMaxRangeInt r) => new(r.Min, r.Max);
        public static implicit operator MinMaxRangeInt(Vector2 vec) => new((int)vec.x, (int)vec.y);
    }
}
