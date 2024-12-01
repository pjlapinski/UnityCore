#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.Patterns.Editor {
[CustomPropertyDrawer(typeof(Option<>), true)]
public class OptionDrawer : PropertyDrawer {
    private const float LabelHeight = 18f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position.y += LabelHeight;

        var indent = EditorGUI.indentLevel;
        ++EditorGUI.indentLevel;

        var isNullField = property.FindPropertyRelative("<IsNone>k__BackingField");
        var isNullFieldHeight = EditorGUI.GetPropertyHeight(isNullField, true);
        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, isNullFieldHeight), isNullField);
        position.y += isNullFieldHeight;

        EditorGUI.BeginDisabledGroup(isNullField.boolValue);
        var valueField = property.FindPropertyRelative("_value");
        if (isNullField.boolValue) {
            if (valueField.isArray)
                valueField.arraySize = 0;
            else
                valueField.boxedValue = default;
        }

        var valueFieldHeight = EditorGUI.GetPropertyHeight(valueField, true);
        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, valueFieldHeight), valueField);
        EditorGUI.EndDisabledGroup();

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        var isNullField = property.FindPropertyRelative("<IsNone>k__BackingField");
        var isNullFieldHeight = EditorGUI.GetPropertyHeight(isNullField, true);
        var valueField = property.FindPropertyRelative("_value");
        var valueFieldHeight = EditorGUI.GetPropertyHeight(valueField, true);
        return LabelHeight + isNullFieldHeight + valueFieldHeight;
    }
}
}
#endif
