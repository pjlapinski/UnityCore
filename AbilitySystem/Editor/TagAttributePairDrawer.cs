#if UNITY_EDITOR
using PJL.GameplayTags;
using UnityEditor;
using UnityEngine;

namespace PJL.AbilitySystem.Editor
{
    [CustomPropertyDrawer(typeof(TagAttributeData))]
    public class TagAttributePairDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var attrField = property.FindPropertyRelative("_initialValue");
            var minField = property.FindPropertyRelative("_min");
            var maxField = property.FindPropertyRelative("_max");

            var oldMin = minField.floatValue;
            var oldMax = maxField.floatValue;

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(((GameplayTag)property.FindPropertyRelative("_tag").boxedValue).ToString());
            ++EditorGUI.indentLevel;
            var attr = EditorGUILayout.FloatField(attrField.displayName, attrField.floatValue);
            var min = EditorGUILayout.FloatField(minField.displayName, minField.floatValue);
            var max = EditorGUILayout.FloatField(maxField.displayName, maxField.floatValue);
            --EditorGUI.indentLevel;
            EditorGUILayout.EndVertical();

            minField.floatValue = min <= max ? min : oldMin;
            maxField.floatValue = max >= min ? max : oldMax;
            attrField.floatValue = Mathf.Clamp(attr, min, max);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            base.GetPropertyHeight(property, label) - EditorGUIUtility.singleLineHeight;
    }
}
#endif
