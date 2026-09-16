#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var _ = new EditorGUI.PropertyScope(position, label, property);
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }
    }
}
#endif
