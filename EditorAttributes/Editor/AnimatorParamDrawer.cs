#if UNITY_EDITOR
using System;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace PJL.EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(AnimatorParamAttribute))]
    public class AnimatorParamDrawer : PropertyDrawer
    {
        private string[] _paramNames, _paramDisplayNames;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _paramNames ??= Array.Empty<string>();
            _paramDisplayNames ??= Array.Empty<string>();

            if (
                !ReflectionUtility.GetFieldValue<Animator>(((AnimatorParamAttribute)attribute).AnimatorName, property).TryUnwrap(out var anim) ||
                anim == null ||
                anim.parameterCount == 0
                )
            {
                EditorGUI.LabelField(position, label);
                return;
            }

            if (_paramNames.Length != anim.parameterCount)
            {
                Array.Resize(ref _paramNames, anim.parameterCount);
                Array.Resize(ref _paramDisplayNames, anim.parameterCount);
            }
            for (var i = 0; i < anim.parameterCount; ++i)
            {
                _paramNames[i] = anim.parameters[i].name;
                _paramDisplayNames[i] = $"{anim.parameters[i].name} ({ParamTypeToString(anim.parameters[i].type)})";
            }

            using var _ = new EditorGUI.PropertyScope(position, label, property);

            if (property.propertyType == SerializedPropertyType.String)
            {
                var idx = _paramNames.IndexOf(property.stringValue);
                idx = EditorGUI.Popup(position, label.text, idx, _paramDisplayNames);
                property.stringValue = _paramNames[idx == -1 ? 0 : idx];
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                var currIdx = anim.parameters.FindIndexOf(p => p.nameHash == property.intValue);
                property.intValue = Animator.StringToHash(_paramNames[EditorGUI.Popup(position, label.text, currIdx == -1 ? 0 : currIdx, _paramDisplayNames)]);
            }
        }

        private static string ParamTypeToString(AnimatorControllerParameterType type) => type switch
        {
            AnimatorControllerParameterType.Float => "Float",
            AnimatorControllerParameterType.Int => "Int",
            AnimatorControllerParameterType.Bool => "Bool",
            AnimatorControllerParameterType.Trigger => "Trigger",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 
            EditorGUIUtility.singleLineHeight;

    }
}
#endif
