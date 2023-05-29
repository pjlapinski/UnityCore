using System.Collections;
using UnityEngine;

namespace PJL.Patterns
{
    public readonly struct FrameValue<T>
    {
        public Option<T> Value { get; }

        public FrameValue(T value, MonoBehaviour reference)
        {
            Value = value;
            reference.StartCoroutine(InvalidateAfterFrame());
        }

        private IEnumerator InvalidateAfterFrame()
        {
            yield return null;
            Value.SetNone();
        }
    }
}
