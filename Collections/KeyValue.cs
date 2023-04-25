using System;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Collections
{
    [Serializable]
    public struct KeyValue<TKey, TValue>
    {
        [field: SerializeField] public TKey Key { get; set; }
        [field: SerializeField] public TValue Value { get; set; }

        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public void Deconstruct(out TKey key, out TValue value)
        {
            key = Key;
            value = Value;
        }

        public static implicit operator KeyValuePair<TKey, TValue>(KeyValue<TKey, TValue> kv) => new(kv.Key, kv.Value);
        public static implicit operator KeyValue<TKey, TValue>(KeyValuePair<TKey, TValue> kv) => new(kv.Key, kv.Value);
    }
}