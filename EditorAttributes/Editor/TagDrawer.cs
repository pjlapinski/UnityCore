#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var _ = new EditorGUI.PropertyScope(position, label, property);
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
        }
    }
}
#endif
