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

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(((GameplayTag)property.FindPropertyRelative("_tag").boxedValue).ToString());
            ++EditorGUI.indentLevel;
            var attr = EditorGUILayout.FloatField(attrField.displayName, attrField.floatValue);
            --EditorGUI.indentLevel;
            EditorGUILayout.EndVertical();

            attrField.floatValue = attr;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            base.GetPropertyHeight(property, label) - EditorGUIUtility.singleLineHeight;
    }
}
#endif
