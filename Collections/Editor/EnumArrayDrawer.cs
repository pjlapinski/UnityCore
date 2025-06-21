#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace PJL.Collections.Editor
{
    [CustomPropertyDrawer(typeof(EnumArray<,>), true)]
    public class EnumArrayDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var enumType = property.boxedValue.GetType().GenericTypeArguments[0];
            var names = Enum.GetNames(enumType);
            var values = property.FindPropertyRelative("_values");

            values.arraySize = names.Length;

            if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label))
            {
                ++EditorGUI.indentLevel;
                for (var i = 0; i < values.arraySize; ++i)
                {
                    EditorGUILayout.PropertyField(values.GetArrayElementAtIndex(i), new GUIContent(names[i]));
                }
                --EditorGUI.indentLevel;
            }

            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
