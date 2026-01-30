using PJL.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PJL.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : Singleton<InputHandler>
    {
        [SerializeField] internal PlayerInput PlayerInput;
        [SerializeField] private InputReceiver _activeReceiver;

        public static InputReceiver ActiveReceiver
        {
            get => Instance._activeReceiver;
            set
            {
                if (Instance._activeReceiver != null)
                    Instance._activeReceiver.OnBecomeInactive.Invoke();
                Instance._activeReceiver = value;
                Instance._activeReceiver.OnBecomeActive.Invoke();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            if (PlayerInput == null) 
                PlayerInput = GetComponent<PlayerInput>();
            if (_activeReceiver != null)
                _activeReceiver.OnBecomeActive.Invoke();
            foreach (var e in PlayerInput.actionEvents)
                e.AddListener(Handle);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_activeReceiver != null)
                _activeReceiver.OnBecomeInactive.Invoke();
            foreach (var e in PlayerInput.actionEvents)
                e.RemoveListener(Handle);
        }

        private void Handle(InputAction.CallbackContext context)
        {
            if (ActiveReceiver == null) return;

            var action = $"{context.action.actionMap.name}.{context.action.name}";
            foreach (var cb in ActiveReceiver.ActionCallbacks)
                if (cb.Action == action)
                    cb.Callback.Invoke(context);
        }
    }
}
