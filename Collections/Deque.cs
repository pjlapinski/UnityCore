using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Collections
{
    [Serializable]
    public class Deque<T> :
        IEnumerable<T>
    {
        [SerializeField, Tooltip("Initial values, runtime values will not be shown in the editor.")] 
        private T[] _initialValues;
        public LinkedList<T> LinkedList { get; private set; }
        
        /// <summary>
        /// Inserts the initial values into the underlying dictionary
        /// </summary>
        public void Initialize()
        {
            foreach (var value in _initialValues) LinkedList.AddLast(value);
            _initialValues = Array.Empty<T>();
        }

        public int Count => LinkedList.Count;

        public T PeekFront() => LinkedList.First.Value;
        public void Push(T value) => LinkedList.AddFirst(value);
        public T Pop()
        {
            var value = LinkedList.First.Value;
            LinkedList.RemoveFirst();
            return value;
        }
        public T PeekBack() => LinkedList.Last.Value;
        public void Enqueue(T value) => LinkedList.AddLast(value);
        public T Dequeue() 
        {
            var value = LinkedList.Last.Value;
            LinkedList.RemoveLast();
            return value;
        }
        public void Clear() => LinkedList.Clear();

        public IEnumerator<T> GetEnumerator() => LinkedList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
