#if UNITY_EDITOR
using System.Linq;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace PJL.EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(SortingLayerAttribute))]
    public class SortingLayerDrawer : PropertyDrawer
    {
        private string[] _layers;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _layers ??= SortingLayer.layers.Select(l => l.name).ToArray();
            using var _ = new EditorGUI.PropertyScope(position, label, property);

            if (property.propertyType == SerializedPropertyType.String)
            {
                var idx = _layers.IndexOf(property.stringValue);
                idx = EditorGUI.Popup(position, label.text, idx, _layers);
                property.stringValue = _layers[idx == -1 ? 0 : idx];
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = EditorGUI.Popup(position, label.text, property.intValue, _layers);
            }
        }
    }
}
#endif
