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

        public EnumArray(IEnumerable<KeyValuePair<TEnum, TValue>> src)
        {
            if (src is EnumArray<TEnum, TValue> ea)
            {
                for (var i = 0; i < ea.Length; ++i)
                    _values[i] = ea._values[i];
                return;
            }

            var l = Enum.GetValues(typeof(TEnum)).Length;
            var arr = src.ToArray();
            for (var i = 0; i < l; ++i)
                _values[i] = arr.ElementAt(i).Value;
        }

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
