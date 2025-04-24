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
    private void Initialize() {
        if (_initialized || !Application.isPlaying) return;
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


    public IEqualityComparer<TKey> Comparer => Dictionary.Comparer;
    public int Count {
        get {
            Initialize();
            return Dictionary.Count;
        }
    }

    public TValue this[TKey key] {
        get {
            Initialize();
            return Dictionary[key];
        }
        set {
            Initialize();
            Dictionary[key] = value;
        }
    }

    public Dictionary<TKey, TValue>.KeyCollection Keys {
        get {
            Initialize();
            return Dictionary.Keys;
        }
    }

    public Dictionary<TKey, TValue>.ValueCollection Values {
        get {
            Initialize();
            return Dictionary.Values;
        }
    }

    public void Add(TKey key, TValue value) {
        Initialize();
        Dictionary.Add(key, value);
    }

    public void Clear() {
        Initialize();
        Dictionary.Clear();
    }

    public bool ContainsKey(TKey key) {
        Initialize();
        return Dictionary.ContainsKey(key);
    }

    public bool ContainsValue(TValue value) {
        Initialize();
        return Dictionary.ContainsValue(value);
    }

    public int EnsureCapacity(int capacity) {
        Initialize();
        return Dictionary.EnsureCapacity(capacity);
    }

    public bool Remove(TKey key) {
        Initialize();
        return Dictionary.Remove(key);
    }

    public bool Remove(TKey key, out TValue value) {
        Initialize();
        return Dictionary.Remove(key, out value);
    }

    public void TrimExcess() {
        Initialize();
        Dictionary.TrimExcess();
    }

    public void TrimExcess(int capacity) {
        Initialize();
        Dictionary.TrimExcess(capacity);
    }

    public bool TryAdd(TKey key, TValue value) {
        Initialize();
        return Dictionary.TryAdd(key, value);
    }

    public bool TryGetValue(TKey key, out TValue value) {
        Initialize();
        return Dictionary.TryGetValue(key, out value);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
        Initialize();
        return Dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        Initialize();
        return GetEnumerator();
    }

    #endregion
}
}
