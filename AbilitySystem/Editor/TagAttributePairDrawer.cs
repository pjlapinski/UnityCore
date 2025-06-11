#if UNITY_EDITOR
using PJL.GameplayTags;
using UnityEditor;
using UnityEngine;

namespace PJL.AbilitySystem.Editor
{
    [CustomPropertyDrawer(typeof(TagAttributePair))]
    public class TagAttributePairDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(((GameplayTag)property.FindPropertyRelative("Tag").boxedValue).ToString());
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Attribute"));
            --EditorGUI.indentLevel;
            EditorGUILayout.EndVertical();

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            base.GetPropertyHeight(property, label) - EditorGUIUtility.singleLineHeight;
    }
}
#endif
