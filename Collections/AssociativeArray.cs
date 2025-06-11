using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PJL.Collections
{
    [Serializable]
    public class AssociativeArray<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        [SerializeField] private List<KeyValue<TKey, TValue>> _values;

        public IEqualityComparer<TKey> Comparer { get; }

        public AssociativeArray()
        {
            _values = new();
            Comparer = EqualityComparer<TKey>.Default;
        }

        public AssociativeArray(IDictionary<TKey, TValue> dictionary) : this()
        {
            _values = new();
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                _values.Add(pair);
        }

        public AssociativeArray(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : this(dictionary)
        {
            Comparer = comparer;
        }

        public AssociativeArray(IEnumerable<KeyValuePair<TKey, TValue>> pairs) : this()
        {
            _values = new();
            foreach (KeyValuePair<TKey, TValue> pair in pairs)
                _values.Add(pair);
        }

        public AssociativeArray(IEnumerable<KeyValuePair<TKey, TValue>> pairs, IEqualityComparer<TKey> comparer)
            : this(pairs)
        {
            Comparer = comparer;
        }

        public AssociativeArray(IEqualityComparer<TKey> comparer)
        {
            Comparer = comparer;
        }

        public AssociativeArray(int capacity) : this()
        {
            _values.Capacity = capacity;
        }

        public AssociativeArray(int capacity, IEqualityComparer<TKey> comparer) : this(capacity)
        {
            Comparer = comparer;
        }

        public int Count => _values.Count;

        public TValue this[TKey key]
        {
            get
            {
                if (!TryGetValue(key, out var value))
                    throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
                return value;
            }
            set
            {
                for (var i = 0; i < _values.Count; i++)
                {
                    if (Comparer.Equals(_values[i].Key, key))
                    {
                        _values[i] = new(key, value);
                        return;
                    }
                }
                _values.Add(new(key, value));
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (!TryAdd(key, value))
                throw new ArgumentException($"An item with the same key has already been added. Key: {key}");
        }

        public void Clear() => _values.Clear();

        public bool ContainsKey(TKey key)
        {
            foreach (var pair in _values)
                if (Comparer.Equals(pair.Key, key))
                    return true;
            return false;
        }

        public bool ContainsValue(TValue value)
        {
            foreach (var pair in _values)
                if (pair.Value.Equals(value))
                    return true;
            return false;
        }

        public int EnsureCapacity(int capacity)
        {
            _values.Capacity = Math.Max(_values.Capacity, capacity);
            return _values.Capacity;
        }

        public bool Remove(TKey key) => Remove(key, out _);

        public bool Remove(TKey key, out TValue value)
        {
            for (var i = 0; i < _values.Count; i++)
            {
                if (Comparer.Equals(_values[i].Key, key))
                {
                    value = _values[i].Value;
                    _values.RemoveAt(i);
                    return true;
                }
            }

            value = default;
            return false;
        }

        public bool TryAdd(TKey key, TValue value)
        {
            foreach (var pair in _values)
                if (Comparer.Equals(pair.Key, key))
                    return false;
            _values.Add(new(key, value));
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            foreach (var pair in _values)
                if (Comparer.Equals(pair.Key, key))
                {
                    value = pair.Value;
                    return true;
                }

            value = default;
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _values
            .Select(kvp => new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value))
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
