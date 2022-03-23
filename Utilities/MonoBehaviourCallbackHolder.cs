using System;
using UnityEngine;
using UnityEngine.Events;

namespace PJL.Utilities
{
    public class MonoBehaviourCallbackHolder : MonoBehaviour
    {
        [SerializeField] private UnityEvent _awakeEvent;
        [SerializeField] private UnityEvent _startEvent;
        [SerializeField] private UnityEvent _onEnableEvent;
        [SerializeField] private UnityEvent _onDisableEvent;
        [SerializeField] private UnityEvent _onDestroyEvent;

        private void Awake() => _awakeEvent.Invoke();
        private void Start() => _startEvent.Invoke();
        private void OnEnable() => _onEnableEvent.Invoke();
        private void OnDisable() => _onDisableEvent.Invoke();
        private void OnDestroy() => _onDestroyEvent.Invoke();
    }
}
