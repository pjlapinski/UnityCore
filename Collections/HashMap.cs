using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Collections {
[Serializable]
public class HashMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> {
    [SerializeField] private KeyValue<TKey, TValue>[] _initialValues;
    private bool _initialized;

    public Dictionary<TKey, TValue> Dictionary { get; private set; }

    /// <summary>
    ///     Inserts the initial values into the underlying dictionary
    /// </summary>
    public void Initialize() {
        if (_initialized) return;
        _initialized = true;
        foreach (var (key, value) in _initialValues) Dictionary[key] = value;
        _initialValues = Array.Empty<KeyValue<TKey, TValue>>();
    }

#if UNITY_EDITOR

    /// <summary>
    ///     Moves all values added to the actual dictionary into the initial values. Useful when using the collection in
    ///     in-editor scripts
    /// </summary>
    public void MoveValuesToInitial() {
        var size = Dictionary.Count;
        _initialValues = new KeyValue<TKey, TValue>[size];
        var i = 0;
        foreach (var kvp in Dictionary) _initialValues[i++] = kvp;
    }

#endif

    #region Dictionary

    public IEqualityComparer<TKey> Comparer => Dictionary.Comparer;
    public int Count => Dictionary.Count;

    public TValue this[TKey key] {
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

    public HashMap() => Dictionary = new Dictionary<TKey, TValue>();

    public HashMap(IDictionary<TKey, TValue> dictionary) => Dictionary = new Dictionary<TKey, TValue>(dictionary);

    public HashMap(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) =>
        Dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);

    public HashMap(IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
        Dictionary = new Dictionary<TKey, TValue>(pairs);

    public HashMap(IEnumerable<KeyValuePair<TKey, TValue>> pairs, IEqualityComparer<TKey> comparer) =>
        Dictionary = new Dictionary<TKey, TValue>(pairs, comparer);

    public HashMap(IEqualityComparer<TKey> comparer) => Dictionary = new Dictionary<TKey, TValue>(comparer);

    public HashMap(int capacity) => Dictionary = new Dictionary<TKey, TValue>(capacity);

    public HashMap(int capacity, IEqualityComparer<TKey> comparer) =>
        Dictionary = new Dictionary<TKey, TValue>(capacity, comparer);

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
}
}
