using System;
using PJL.Patterns.Editor;
using UnityEngine;
using UnityEngine.Events;

namespace PJL.Patterns
{
    public abstract class EventBusCallback<T> : MonoBehaviour where T : IEvent
    {
        [SerializeField] private UnityEvent<T> _callback;

        private void Awake() => EventBus<T>.Subscribe(Callback);

        private void OnDestroy() => EventBus<T>.Unsubscribe(Callback);

        private void Callback(T data) => _callback?.Invoke(data);
    }
}
