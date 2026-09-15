#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(DisableIfAttribute))]
    public class DisableIfDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var v = ReflectionUtility.ResolveCondition(((DisableIfAttribute)attribute).Condition, property);

            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.BeginDisabledGroup(v);
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, label);
    }
}
#endif
