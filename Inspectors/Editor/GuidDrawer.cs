#if UNITY_EDITOR
using System;
using PJL.Debug;
using UnityEditor;
using UnityEngine;

namespace PJL.Inspectors.Editor {
[CustomPropertyDrawer(typeof(GuidAttribute))]
public class GuidDrawer : PropertyDrawer {
    private const float HorizontalSpacing = 3f;
    private const float TxtSizePercent = .52f;
    private const float BtnSizePercent = .12f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position.y += EditorGUIUtility.singleLineHeight;

        var x = position.x;
        var txtRect = new Rect(x, position.y, position.width * TxtSizePercent, EditorGUIUtility.singleLineHeight);
        GUI.enabled = false;
        if (!Guid.TryParse(property.stringValue, out var _)) {
            property.stringValue = Guid.Empty.ToString();
        }
        EditorGUI.TextField(txtRect, property.stringValue);
        GUI.enabled = true;
        x += txtRect.width + HorizontalSpacing;

        var btnWidth = position.width * BtnSizePercent - HorizontalSpacing;
        var copyRect = new Rect(x, position.y, btnWidth, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(copyRect, new GUIContent("Copy"))) {
            EditorGUIUtility.systemCopyBuffer = property.stringValue;
        }
        x += btnWidth + HorizontalSpacing;

        var pasteRect = new Rect(x, position.y, btnWidth, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(pasteRect, new GUIContent("Paste"))) {
            if (Guid.TryParse(EditorGUIUtility.systemCopyBuffer, out var guid)) {
                property.stringValue = guid.ToString();
            } else {
                ContextLogger.Log(LogType.Error, "EDITOR", "Tried to paste an invalid GUID.");
            }
        }
        x += btnWidth + HorizontalSpacing;

        var newRect = new Rect(x, position.y, btnWidth, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(newRect, new GUIContent("New"))) {
            property.stringValue = Guid.NewGuid().ToString();
        }
        x += btnWidth + HorizontalSpacing;

        var emptyRect = new Rect(x, position.y, btnWidth, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(emptyRect, new GUIContent("Empty"))) {
            property.stringValue = Guid.Empty.ToString();
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUIUtility.singleLineHeight * 2;
    }
}
}
#endif
