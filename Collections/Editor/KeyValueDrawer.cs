#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.Collections.Editor
{
    [CustomPropertyDrawer(typeof(KeyValue<,>), true)]
    public class KeyValueDrawer : PropertyDrawer
    {
        private const float KeyLabelWidth = 40f;
        private const float WidthLabelWidth = 50f;
        private const float LabelHeight = 18f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            position.y += LabelHeight;

            var indent = EditorGUI.indentLevel;
            ++EditorGUI.indentLevel;
            var inputWidth = (position.width - KeyLabelWidth - WidthLabelWidth) / 2;

            var keyLabelRect = new Rect(position.x, position.y, KeyLabelWidth, LabelHeight);
            var keyField = property.FindPropertyRelative("<Key>k__BackingField");
            var keyFieldHeight = EditorGUI.GetPropertyHeight(keyField);
            var keyRect = new Rect(position.x + KeyLabelWidth, position.y, inputWidth, keyFieldHeight);

            var valueLabelRect = new Rect(position.x + KeyLabelWidth + inputWidth, position.y, WidthLabelWidth, 18f);
            var valueField = property.FindPropertyRelative("<Value>k__BackingField");
            var valueFieldHeight = EditorGUI.GetPropertyHeight(valueField);
            var valueRect = new Rect(position.x + KeyLabelWidth + inputWidth + WidthLabelWidth, position.y, inputWidth,
                valueFieldHeight);

            EditorGUI.LabelField(keyLabelRect, "Key");
            EditorGUI.PropertyField(keyRect, keyField, GUIContent.none);
            EditorGUI.LabelField(valueLabelRect, "Value");
            EditorGUI.PropertyField(valueRect, valueField, GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var keyField = property.FindPropertyRelative("<Key>k__BackingField");
            var keyFieldHeight = EditorGUI.GetPropertyHeight(keyField);
            var valueField = property.FindPropertyRelative("<Value>k__BackingField");
            var valueFieldHeight = EditorGUI.GetPropertyHeight(valueField);
            return LabelHeight + Mathf.Max(keyFieldHeight, valueFieldHeight);
        }
    }
}
#endif