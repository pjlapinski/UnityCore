using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Collections
{
    [Serializable]
    public class Set<T> : IEnumerable<T>
    {
        [SerializeField] private T[] _initialValues;
        private bool _initialized;

        public HashSet<T> HashSet { get; private set; }

        /// <summary>
        ///     Inserts the initial values into the underlying HashSet
        /// </summary>
        private void Initialize()
        {
            if (_initialized || !Application.isPlaying) return;
            _initialized = true;
            foreach (var value in _initialValues) HashSet.Add(value);
            _initialValues = Array.Empty<T>();
        }

#if UNITY_EDITOR

        /// <summary>
        ///     Moves all values added to the actual hash set into the initial values. Useful when using the collection in
        ///     in-editor scripts
        /// </summary>
        public void MoveValuesToInitial()
        {
            var size = HashSet.Count;
            _initialValues = new T[size];
            var i = 0;
            foreach (var value in HashSet) _initialValues[i++] = value;
        }

#endif

        #region HashSet

        public Set()
        {
            HashSet = new HashSet<T>();
        }

        public Set(IEnumerable<T> enumerable)
        {
            HashSet = new HashSet<T>(enumerable);
        }

        public Set(IEnumerable<T> enumerable, IEqualityComparer<T> comparer)
        {
            HashSet = new HashSet<T>(enumerable, comparer);
        }

        public Set(IEqualityComparer<T> comparer)
        {
            HashSet = new HashSet<T>(comparer);
        }

        public Set(int capacity)
        {
            HashSet = new HashSet<T>(capacity);
        }

        public Set(int capacity, IEqualityComparer<T> comparer)
        {
            HashSet = new HashSet<T>(capacity, comparer);
        }

        public IEqualityComparer<T> Comparer
        {
            get
            {
                Initialize();
                return HashSet.Comparer;
            }
        }

        public int Count
        {
            get
            {
                Initialize();
                return HashSet.Count;
            }
        }

        public void Add(T value)
        {
            Initialize();
            HashSet.Add(value);
        }

        public void Clear()
        {
            Initialize();
            HashSet.Clear();
        }

        public bool Contains(T value)
        {
            Initialize();
            return HashSet.Contains(value);
        }

        public int EnsureCapacity(int capacity)
        {
            Initialize();
            return HashSet.EnsureCapacity(capacity);
        }

        public bool Remove(T value)
        {
            Initialize();
            return HashSet.Remove(value);
        }

        public void TrimExcess()
        {
            Initialize();
            HashSet.TrimExcess();
        }

        public bool TryGetValue(T value, out T outValue)
        {
            Initialize();
            return HashSet.TryGetValue(value, out outValue);
        }

        public void UnionWith(Set<T> other)
        {
            Initialize();
            HashSet.UnionWith(other.HashSet);
        }

        public void IntersectWith(Set<T> other)
        {
            Initialize();
            HashSet.IntersectWith(other.HashSet);
        }

        public void ExceptWith(Set<T> other)
        {
            Initialize();
            HashSet.ExceptWith(other.HashSet);
        }

        public void SymmetricExceptWith(Set<T> other)
        {
            Initialize();
            HashSet.SymmetricExceptWith(other.HashSet);
        }

        public bool Overlaps(Set<T> other)
        {
            Initialize();
            return HashSet.Overlaps(other.HashSet);
        }

        public bool IsSubsetOf(Set<T> other)
        {
            Initialize();
            return HashSet.IsSubsetOf(other.HashSet);
        }

        public bool IsProperSubsetOf(Set<T> other)
        {
            Initialize();
            return HashSet.IsProperSubsetOf(other.HashSet);
        }

        public bool IsSupersetOf(Set<T> other)
        {
            Initialize();
            return HashSet.IsSupersetOf(other.HashSet);
        }

        public bool IsProperSupersetOf(Set<T> other)
        {
            Initialize();
            return HashSet.IsProperSupersetOf(other.HashSet);
        }

        public bool SetEquals(Set<T> other)
        {
            Initialize();
            return HashSet.SetEquals(other.HashSet);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Initialize();
            return HashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Initialize();
            return GetEnumerator();
        }

        #endregion
    }
}