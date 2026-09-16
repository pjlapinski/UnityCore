#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = ((LabelAttribute)attribute).Text;
            using var _ = new EditorGUI.PropertyScope(position, label, property);
            EditorGUI.PropertyField(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, label);
    }
}
#endif
