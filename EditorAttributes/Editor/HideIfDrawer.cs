#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var v = ReflectionUtility.ResolveCondition(((HideIfAttribute)attribute).Condition, property);

            EditorGUI.BeginProperty(position, label, property);

            if (!v) EditorGUI.PropertyField(position, property, label);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            ReflectionUtility.ResolveCondition(((HideIfAttribute)attribute).Condition, property) ? 0f : EditorGUI.GetPropertyHeight(property, label);
    }
}
#endif
