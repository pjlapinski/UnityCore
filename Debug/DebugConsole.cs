using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PJL.Debug
{
    public class DebugConsole : IDisposable
    {
        public int FontSize { get; set; } = 12;
        private readonly List<ICheat> _cheats;
        private readonly List<string> _history;

        private readonly List<string> _logs;
        private int _historyIndex;
        private bool _newLog;
        private Vector2 _scrollPosition;
        private string _textField;
        private bool _visible;

        public DebugConsole()
        {
            _logs = new();
            _history = new();
            _cheats = new();
            _scrollPosition = Vector2.zero;
            UnityEngine.Debug.developerConsoleEnabled = false;
        }

        public bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                _historyIndex = -1;
                _textField = "";
            }
        }

        public void Dispose()
        {
            UnityEngine.Debug.developerConsoleEnabled = true;
        }

        public void RegisterCommands(Type assemblyMarker)
        {
            var assembly = assemblyMarker.Assembly;
            var cheatType = typeof(ICheat);
            foreach (var type in assembly.GetTypes())
            {
                if (!cheatType.IsAssignableFrom(type)) continue;
                _cheats.Add(Activator.CreateInstance(type) as ICheat);
            }
        }

        public void Draw()
        {
            if (!Visible)
            {
                if ((_textField?.Length ?? 0) > 0) _textField = string.Empty;
                return;
            }

            const int padding = 5;
            const float confirmWidth = .1f;
            var inputHeight = FontSize * 1.5f;
            var backgroundRect = new Rect(0, 0, Screen.width, Screen.height * .6f);
            var scrollRect = new Rect(padding, padding, backgroundRect.width - 2 * padding,
                backgroundRect.height - 2 * padding);
            var textFieldWidth = backgroundRect.width * (1 - confirmWidth);
            var textFieldRect = new Rect(0, backgroundRect.height, textFieldWidth, inputHeight);
            var confirmBtnRect = new Rect(textFieldWidth, backgroundRect.height, backgroundRect.width * confirmWidth, inputHeight);
            var fullHeight = _logs
                .Select(log => new GUIContent(log))
                .Sum(content => GUI.skin.label.CalcHeight(content, backgroundRect.width));

            GUI.skin.label.fontSize = GUI.skin.textField.fontSize = FontSize;
            GUI.Box(backgroundRect, string.Empty);
            _scrollPosition = GUI.BeginScrollView(
                scrollRect,
                _scrollPosition,
                new Rect(0, Mathf.Min(fullHeight - scrollRect.height, 0), scrollRect.width, fullHeight),
                GUIStyle.none,
                GUI.skin.verticalScrollbar
            );
            var y = 0f;
            foreach (var log in _logs)
            {
                var content = new GUIContent(log);
                var height = GUI.skin.label.CalcHeight(content, scrollRect.width);
                GUI.Label(new Rect(0, y, scrollRect.width, height), content);
                y += height;
            }

            GUI.EndScrollView();
            _textField = GUI.TextField(textFieldRect, _textField);
            if (GUI.Button(confirmBtnRect, new GUIContent("->")))
            {
                ConfirmInput();
            }

            if (_newLog)
            {
                _scrollPosition = new Vector2(0, fullHeight);
                _newLog = false;
            }
        }

        public void MoveHistory(int value)
        {
            if (_historyIndex == -1)
            {
                if (value > 0)
                    _historyIndex = value - 1;
                else
                    _historyIndex = _history.Count + value;
            }
            else
            {
                _historyIndex += value;
            }

            if (_historyIndex < 0)
            {
                _historyIndex = -1;
                return;
            }

            if (_historyIndex >= _history.Count)
            {
                _historyIndex = _history.Count - 1;
                return;
            }

            _textField = _history[_historyIndex];
        }

        public void LogReceived(string message, string trace, LogType type)
        {
            _logs.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            _newLog = true;
        }

        public void ConfirmInput()
        {
            if (_historyIndex >= 0)
            {
                _history.RemoveRange(_historyIndex, _history.Count - _historyIndex);
                _historyIndex = -1;
            }

            _history.Add(_textField);
            var parts = _textField.Split(' ');
            _textField = string.Empty;
            var foundAny = false;
            foreach (var cheat in _cheats)
                if (parts[0] == cheat.Command)
                {
                    if (parts.Length - 1 == cheat.NumArgs && cheat.TryExecute(parts.Skip(1))) return;
                    foundAny = true;
                }

            if (!foundAny) ContextLogger.LogFormat(LogType.Error, "DEBUG", "Unrecognised command: {0}", parts[0]);
        }
    }
}
