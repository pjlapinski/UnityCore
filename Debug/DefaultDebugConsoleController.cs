using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PJL.Debug
{
    public class DefaultDebugConsoleController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _commandsAssemblyMarker;
        private DebugConsole _console;
        private Dictionary<Key, bool> _keyStates;

        public int FontSize
        {
            get => _console.FontSize;
            set => _console.FontSize = value;
        }

        private bool KeyPressed(Key key) => 
            _keyStates.TryGetValue(key, out var state) &&
            state != Keyboard.current[key].isPressed &&
            !Keyboard.current[key].isPressed;

        private void Awake()
        {
            _console = new();
            if (Keyboard.current != null)
            {
                _keyStates = new();
                _keyStates[Key.Backquote] = Keyboard.current.backquoteKey.isPressed;
                _keyStates[Key.Enter] = Keyboard.current.enterKey.isPressed;
            }
            if (_commandsAssemblyMarker != null)
                _console.RegisterCommands(_commandsAssemblyMarker.GetType());
            Application.logMessageReceived += _console.LogReceived;
        }

        private void Update()
        {
            if (Keyboard.current == null)
                return;

            if (KeyPressed(Key.Backquote))
                _console.Visible = !_console.Visible;

            if (_console.Visible && KeyPressed(Key.Enter))
                _console.ConfirmInput();

            _keyStates[Key.Backquote] = Keyboard.current.backquoteKey.isPressed;
            _keyStates[Key.Enter] = Keyboard.current.enterKey.isPressed;
        }

        private void OnGUI() => _console.Draw();

        private void OnDestroy()
        {
            Application.logMessageReceived -= _console.LogReceived;
            _console.Dispose();
        }
    }
}
