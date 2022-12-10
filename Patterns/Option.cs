using System;
using UnityEngine;
namespace PJL.Patterns
{
    [Serializable]
    public struct Option<T>
    {
        [field: SerializeField] public bool IsNone { get; private set; }
        [SerializeField] private T _value;

        public static Option<T> None => new() { IsNone = true, _value = default };

        public void Match(Action ifNone, Action<T> ifSome)
        {
            if (IsNone || _value == null) ifNone();
            else ifSome(_value);
        }

        public void SetNone()
        {
            IsNone = true;
            _value = default;
        }

        public void SetValue(T value)
        {
            if (value == null)
            {
                IsNone = true;
                _value = default;
            }
            else
            {
                IsNone = false;
                _value = value;
            }
        }

        public static implicit operator Option<T>(T value)
        {
            if (value == null)
            {
                return None;
            }

            return new Option<T>
            {
                IsNone = false,
                _value = value,
            };
        }
    }
}
