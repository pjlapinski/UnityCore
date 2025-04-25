#if UNITY_EDITOR
using PJL.Data;
using UnityEditor;
using UnityEngine;

namespace PJL.Inspectors.Editor {
[CustomPropertyDrawer(typeof(Uuid))]
public class UuidDrawer : PropertyDrawer {
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
        var value = (Uuid)property.boxedValue;
        EditorGUI.TextField(txtRect, value.ToString());
        GUI.enabled = true;
        x += txtRect.width + HorizontalSpacing;

        var btnWidth = position.width * BtnSizePercent - HorizontalSpacing;
        var copyRect = new Rect(x, position.y, btnWidth, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(copyRect, new GUIContent("Copy"))) {
            EditorGUIUtility.systemCopyBuffer = value.ToString();
        }
        x += btnWidth + HorizontalSpacing;

        var pasteRect = new Rect(x, position.y, btnWidth, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(pasteRect, new GUIContent("Paste"))) {
            if (Uuid.TryParse(EditorGUIUtility.systemCopyBuffer, out var guid)) {
                property.boxedValue = guid;
            } else {
                Debug.LogError("Tried to paste an invalid GUID.");
            }
        }
        x += btnWidth + HorizontalSpacing;

        var newRect = new Rect(x, position.y, btnWidth, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(newRect, new GUIContent("New"))) {
            property.boxedValue = Uuid.NewUuid();
        }
        x += btnWidth + HorizontalSpacing;

        var emptyRect = new Rect(x, position.y, btnWidth, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(emptyRect, new GUIContent("Empty"))) {
            property.boxedValue = Uuid.Empty;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUIUtility.singleLineHeight * 2;
    }
}
}
#endif
