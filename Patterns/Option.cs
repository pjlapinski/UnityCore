using System;
using UnityEngine;

namespace PJL.Patterns
{
    [Serializable]
    public struct Option<T>
    {
        [SerializeField] private bool _isNone;
        [SerializeField] private T _value;

        public bool IsNone
        {
            get => _isNone || _value == null;
            private set
            {
                _isNone = value;
                if (value) _value = default;
            }
        }

        public static Option<T> None => new() {_isNone = true, _value = default};

        public void Match(Action ifNone, Action<T> ifSome)
        {
            if (IsNone) ifNone();
            else ifSome(_value);
        }

        public bool TryUnwrap(out T value)
        {
            if (IsNone)
            {
                value = default;
                return false;
            }

            value = _value;
            return true;
        }

        public void SetNone() => IsNone = true;

        public void SetValue(T value)
        {
            if (value == null)
            {
                SetNone();
            }
            else
            {
                _isNone = false;
                _value = value;
            }
        }

        public static implicit operator Option<T>(T value)
        {
            if (value == null) return None;

            return new Option<T>
            {
                _isNone = false,
                _value = value,
            };
        }
    }
}
