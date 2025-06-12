#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(IndentAttribute))]
    public class IndentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as IndentAttribute;
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.indentLevel += attr.AddLevel;
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.indentLevel -= attr.AddLevel;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
           EditorGUI.GetPropertyHeight(property, label);
    }
}
#endif
