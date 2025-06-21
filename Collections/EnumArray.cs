using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PJL.Collections
{
// Acts like a Dictionary<TEnum, TValue>
    [Serializable]
    public class EnumArray<TEnum, TValue> : IEnumerable<KeyValuePair<TEnum, TValue>> where TEnum : struct, Enum
    {
        [SerializeField] private TValue[] _values = new TValue[Enum.GetValues(typeof(TEnum)).Length];

        public int Length => _values.Length;
        public long LongLength => _values.LongLength;

        public TValue this[TEnum key]
        {
            get => _values[(int)Convert.ChangeType(key, typeof(int))];
            set => _values[(int)Convert.ChangeType(key, typeof(int))] = value;
        }

        public TValue this[int index]
        {
            get => _values[index];
            set => _values[index] = value;
        }

        public IEnumerator<KeyValuePair<TEnum, TValue>> GetEnumerator()
        {
            var enumType = typeof(TEnum);
            return _values
                .Select((val, i) => new KeyValuePair<TEnum, TValue>((TEnum)Enum.ToObject(enumType, i), val))
                .GetEnumerator();
        }

        public static explicit operator TValue[](EnumArray<TEnum, TValue> array) => array._values;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
