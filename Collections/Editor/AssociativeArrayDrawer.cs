#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.Collections.Editor
{
    [CustomPropertyDrawer(typeof(AssociativeArray<,>), true)]
    public class AssociativeArrayDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valuesField = property.FindPropertyRelative("_values");
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.PropertyField(position, valuesField, new GUIContent(label.text));

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_values"), label);
    }
}
#endif
