using System;
using UnityEngine;
using UnityEngine.Events;

namespace PJL.Data
{
    [Serializable]
    public struct Reactive<T>
    {
        [SerializeField] private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChange?.Invoke(_value);
            }
        }

        [field: SerializeField] public UnityEvent<T> OnValueChange { get; private set; }
    }
}