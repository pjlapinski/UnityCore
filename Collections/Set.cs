using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Collections
{
    [Serializable]
    public class Set<T> : 
        IEnumerable<T>
    {
        [SerializeField, Tooltip("Initial values, runtime values will not be shown in the editor.")] 
        private T[] _initialValues;
        public HashSet<T> HashSet { get; private set; }

        /// <summary>
        /// Inserts the initial values into the underlying dictionary
        /// </summary>
        public void Initialize()
        {
            foreach (var value in _initialValues) HashSet.Add(value);
            _initialValues = Array.Empty<T>();
        }

        #region HashSet

        public IEqualityComparer<T> Comparer => HashSet.Comparer;
        public int Count => HashSet.Count;

        public void Add(T value) => HashSet.Add(value);
        public void Clear() => HashSet.Clear();
        public bool Contains(T value) => HashSet.Contains(value);
        public int EnsureCapacity(int capacity) => HashSet.EnsureCapacity(capacity);
        public bool Remove(T value) => HashSet.Remove(value);
        public void TrimExcess() => HashSet.TrimExcess();
        public bool TryGetValue(T value, out T outValue) => HashSet.TryGetValue(value, out outValue);
        public void UnionWith(Set<T> other) => HashSet.UnionWith(other.HashSet);
        public void IntersectWith(Set<T> other) => HashSet.IntersectWith(other.HashSet);
        public void ExceptWith(Set<T> other) => HashSet.ExceptWith(other.HashSet);
        public void SymmetricExceptWith(Set<T> other) => HashSet.SymmetricExceptWith(other.HashSet);
        public bool Overlaps(Set<T> other) => HashSet.Overlaps(other.HashSet);
        public bool IsSubsetOf(Set<T> other) => HashSet.IsSubsetOf(other.HashSet);
        public bool IsProperSubsetOf(Set<T> other) => HashSet.IsProperSubsetOf(other.HashSet);
        public bool IsSupersetOf(Set<T> other) => HashSet.IsSupersetOf(other.HashSet);
        public bool IsProperSupersetOf(Set<T> other) => HashSet.IsProperSupersetOf(other.HashSet);
        public bool SetEquals(Set<T> other) => HashSet.SetEquals(other.HashSet);
        public Set() => HashSet = new();
        public Set(IEnumerable<T> enumerable) => HashSet = new(enumerable);
        public Set(IEnumerable<T> enumerable, IEqualityComparer<T> comparer) => HashSet = new(enumerable, comparer);
        public Set(IEqualityComparer<T> comparer) => HashSet = new(comparer);
        public Set(int capacity) => HashSet = new(capacity);
        public Set(int capacity, IEqualityComparer<T> comparer) => HashSet = new(capacity, comparer);

        public IEnumerator<T> GetEnumerator() => HashSet.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
