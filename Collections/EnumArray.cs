using System;
using System.Collections;
using System.Collections.Generic;

namespace PJL.Collections
{
    // Acts like a Dictionary<TEnum, TValue>
    [Serializable]
    public class EnumArray<TEnum, TValue> : IEnumerable<TValue> where TEnum : struct, Enum
    {
        [SerializeField]
        private TValue[] _values;

        public int Length => _values.Length;
        public long LongLength => _values.LongLength;

        public EnumArray()
        {
            _values = new TValue[Enum.GetValues(typeof(TEnum)).Length];
        }

        public TValue this[TEnum key]
        {
            get => _values[(int)Convert.ChangeType(key, typeof(int))];
            set => _values[(int)Convert.ChangeType(key, typeof(int))] = value;
        }

        public IEnumerator<TValue> GetEnumerator() => ((IEnumerable<TValue>) _values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
