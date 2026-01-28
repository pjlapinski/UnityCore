using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PJL.Input
{
    [Serializable]
    internal class InputCallback
    {
#if UNITY_EDITOR
        private DropdownList<string> InputActions()
        {
            var list = new DropdownList<string>();
            foreach (var action in InputHandler.Instance.PlayerInput.actions)
            {
                var val = action.actionMap.name + "." + action.name;
                list.Add(val, val);
            }

            return list;
        }

        [Dropdown(nameof(InputActions))]
#endif
        public string Action;
        public UnityEvent<InputAction.CallbackContext> Callback;
    }

    public class InputReceiver : MonoBehaviour
    {
        [SerializeField] internal InputCallback[] ActionCallbacks;
        [field: SerializeField] public UnityEvent OnBecomeActive { get; set; }
        [field: SerializeField] public UnityEvent OnBecomeInactive { get; set; }
    }
}
