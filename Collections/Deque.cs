using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Collections {
[Serializable]
public class Deque<T> : IEnumerable<T> {
    [SerializeField] private T[] _initialValues;
    private bool _initialized;

    public LinkedList<T> LinkedList { get; private set; }

    public int Count => LinkedList.Count;

    public IEnumerator<T> GetEnumerator() => LinkedList.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    ///     Inserts the initial values into the underlying linked list
    /// </summary>
    private void Initialize() {
        if (_initialized || !Application.isPlaying) return;
        _initialized = true;
        foreach (var value in _initialValues) LinkedList.AddLast(value);
        _initialValues = Array.Empty<T>();
    }

#if UNITY_EDITOR

    /// <summary>
    ///     Moves all values added to the actual linked list into the initial values. Useful when using the collection in
    ///     in-editor scripts
    /// </summary>
    public void MoveValuesToInitial() {
        var size = LinkedList.Count;
        _initialValues = new T[size];
        var i = 0;
        foreach (var value in LinkedList) _initialValues[i++] = value;
    }

#endif

    public T PeekFront() {
        Initialize();
        return LinkedList.First.Value;
    }

    public void Push(T value) {
        Initialize();
        LinkedList.AddFirst(value);
    }

    public T Pop() {
        Initialize();
        var value = LinkedList.First.Value;
        LinkedList.RemoveFirst();
        return value;
    }

    public T PeekBack() {
        Initialize();
        return LinkedList.Last.Value;
    }

    public void Enqueue(T value) {
        Initialize();
        LinkedList.AddLast(value);
    }

    public T Dequeue() {
        var value = LinkedList.Last.Value;
        LinkedList.RemoveLast();
        return value;
    }

    public void Clear() {
        Initialize();
        LinkedList.Clear();
    }
}
}
