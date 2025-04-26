using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PJL.Debug {
public class DebugConsole {
    private bool _visible;
    public bool Visible {
        get => _visible;
        set {
            _visible = value;
            _historyIndex = -1;
            _textField = "";
        }
    }

    private readonly List<string> _logs;
    private readonly List<string> _history;
    private readonly List<ICheat> _cheats;
    private Vector2 _scrollPosition;
    private bool _newLog;
    private string _textField;
    private int _historyIndex;

    private const int FontSize = 32;

    public DebugConsole() {
        _logs = new();
        _history = new();
        _cheats = new();
        _scrollPosition = Vector2.zero;
        UnityEngine.Debug.developerConsoleEnabled = false;
    }

    ~DebugConsole() {
        UnityEngine.Debug.developerConsoleEnabled = true;
    }

    public void RegisterCommands(Type assemblyReference) {
        var assembly = assemblyReference.Assembly;
        var cheatType = typeof(ICheat);
        foreach (var type in assembly.GetTypes()) {
            if (!cheatType.IsAssignableFrom(type)) {
                continue;
            }
            _cheats.Add(Activator.CreateInstance(type) as ICheat);
        }
    }

    public void Draw() {
        if (!Visible) {
            if ((_textField?.Length ?? 0) > 0) {
                _textField = string.Empty;
            }
            return;
        }

        const int padding = 5;
        const int inputHeight = FontSize + FontSize / 2;
        var backgroundRect = new Rect(0, 0, Screen.width, Screen.height * .6f);
        var scrollRect = new Rect(padding, padding, backgroundRect.width - 2 * padding, backgroundRect.height - 2 * padding);
        var textFieldRect = new Rect(0, backgroundRect.height, backgroundRect.width, inputHeight);
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
        foreach (var log in _logs) {
            var content = new GUIContent(log);
            var height = GUI.skin.label.CalcHeight(content, scrollRect.width);
            GUI.Label(new Rect(0, y, scrollRect.width, height), content);
            y += height;
        }
        GUI.EndScrollView();
        _textField = GUI.TextField(textFieldRect, _textField);

        if (_newLog) {
            _scrollPosition = new Vector2(0, fullHeight);
            _newLog = false;
        }
    }

    public void MoveHistory(int value) {
        if (_historyIndex == -1) {
            if (value > 0) {
                _historyIndex = value - 1;
            } else {
                _historyIndex = _history.Count + value;
            }
        } else {
            _historyIndex += value;
        }
        if (_historyIndex < 0) {
            _historyIndex = -1;
            return;
        }

        if (_historyIndex >= _history.Count) {
            _historyIndex = _history.Count - 1;
            return;
        }
        _textField = _history[_historyIndex];
    }

    public void LogReceived(string message, string trace, LogType type) {
        _logs.Add(message);
        _newLog = true;
    }

    public void ConfirmInput() {
        if (_historyIndex >= 0) {
            _history.RemoveRange(_historyIndex, _history.Count - _historyIndex);
            _historyIndex = -1;
        }
        _history.Add(_textField);
        var parts =  _textField.Split(' ');
        _textField = string.Empty;
        var foundAny = false;
        foreach (var cheat in _cheats) {
            if (parts[0] == cheat.Command) {
                if (parts.Length - 1 == cheat.NumArgs && cheat.TryExecute(parts.Skip(1))) {
                    return;
                }
                foundAny = true;
            }
        }

        if (!foundAny) {
            ContextLogger.LogFormat(LogType.Error, "DEBUG", "Unrecognised command: {0}", parts[0]);
        }
    }
}
}
