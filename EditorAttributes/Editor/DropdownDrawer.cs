#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace PJL.EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(DropdownAttribute))]
    public class DropdownDrawer : PropertyDrawer
    {
        private string[] _options;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_options == null) GetOptions(property);

            using var _ = new EditorGUI.PropertyScope(position, label, property);
            if (_options == null)
            {
                EditorGUI.LabelField(position, label);
                return;
            }

            if (property.propertyType == SerializedPropertyType.String)
            {
                var idx = _options.IndexOf(property.stringValue);
                idx = EditorGUI.Popup(position, label.text, idx, _options);
                property.stringValue = _options[idx == -1 ? 0 : idx];
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = EditorGUI.Popup(position, label.text, property.intValue, _options);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUIUtility.singleLineHeight;

        private void GetOptions(SerializedProperty prop)
        {
            var val = ReflectionUtility.GetFieldValue<IEnumerable<string>>(((DropdownAttribute)attribute).Source, prop);
            if (val.TryUnwrap(out var opt))
                _options = opt.ToArray();
        }
    }
}
#endif
