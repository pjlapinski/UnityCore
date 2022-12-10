using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Collections
{
    [Serializable]
    public class HashMap<TKey, TValue> : 
        IEnumerable<KeyValuePair<TKey, TValue>>
    {
        [SerializeField, Tooltip("Initial values, runtime values will not be shown in the editor.")] 
        private KeyValue<TKey, TValue>[] _initialValues;
        public Dictionary<TKey, TValue> Dictionary { get; private set; }

        /// <summary>
        /// Inserts the initial values into the underlying dictionary
        /// </summary>
        public void Initialize()
        {
            foreach (var (key, value) in _initialValues) Dictionary[key] = value;
            _initialValues = Array.Empty<KeyValue<TKey, TValue>>();
        }

        #region Dictionary

        public IEqualityComparer<TKey> Comparer => Dictionary.Comparer;
        public int Count => Dictionary.Count;
        public TValue this[TKey key]
        {
            get => Dictionary[key];
            set => Dictionary[key] = value;
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys => Dictionary.Keys;
        public Dictionary<TKey, TValue>.ValueCollection Values => Dictionary.Values;

        public void Add(TKey key, TValue value) => Dictionary.Add(key, value);
        public void Clear() => Dictionary.Clear();
        public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);
        public bool ContainsValue(TValue value) => Dictionary.ContainsValue(value);
        public int EnsureCapacity(int capacity) => Dictionary.EnsureCapacity(capacity);
        public bool Remove(TKey key) => Dictionary.Remove(key);
        public bool Remove(TKey key, out TValue value) => Dictionary.Remove(key, out value);
        public void TrimExcess() => Dictionary.TrimExcess();
        public void TrimExcess(int capacity) => Dictionary.TrimExcess(capacity);
        public bool TryAdd(TKey key, TValue value) => Dictionary.TryAdd(key, value);
        public bool TryGetValue(TKey key, out TValue value) => Dictionary.TryGetValue(key, out value);

        public HashMap() => Dictionary = new();
        public HashMap(IDictionary<TKey, TValue> dictionary) => Dictionary = new(dictionary);
        public HashMap(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) => Dictionary = new(dictionary, comparer);
        public HashMap(IEnumerable<KeyValuePair<TKey, TValue>> pairs) => Dictionary = new(pairs);
        public HashMap(IEnumerable<KeyValuePair<TKey, TValue>> pairs, IEqualityComparer<TKey> comparer) => Dictionary = new(pairs, comparer);
        public HashMap(IEqualityComparer<TKey> comparer) => Dictionary = new(comparer);
        public HashMap(int capacity) => Dictionary = new(capacity);
        public HashMap(int capacity, IEqualityComparer<TKey> comparer) => Dictionary = new(capacity, comparer);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
