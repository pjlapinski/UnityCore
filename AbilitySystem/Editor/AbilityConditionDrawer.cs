#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace PJL.AbilitySystem.Editor
{
    [CustomPropertyDrawer(typeof(AbilityCondition), true)]
    public class AbilityConditionDrawer : PropertyDrawer
    {
        private Dictionary<string, Type> _inheritors;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_inheritors == null) BuildInheritorsMap();

            var typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var dataRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, position.height - EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginProperty(position, label, property);

            var typeName = GetShortTypeName(property.managedReferenceFullTypename);
            if (EditorGUI.DropdownButton(typeRect, new GUIContent(typeName ?? "Select AbilityCondition type"), FocusType.Keyboard))
            {
                var menu = new GenericMenu();
                if (_inheritors == null || _inheritors.Count == 0)
                {
                    menu.AddDisabledItem(new GUIContent("No AbilityConditions available"));
                }
                else
                {
                    foreach (var (name, type) in _inheritors)
                    {
                        menu.AddItem(new GUIContent(name), type.FullName == typeName, () =>
                        {
                            property.managedReferenceValue = Activator.CreateInstance(type);
                            property.serializedObject.ApplyModifiedProperties();
                        });
                    }
                }
                menu.ShowAsContext();
            }

            if (property.managedReferenceValue != null)
            {
                ++EditorGUI.indentLevel;
                EditorGUI.PropertyField(dataRect, property, new GUIContent("Data"), true);
                --EditorGUI.indentLevel;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight;

        private void BuildInheritorsMap() =>
            _inheritors = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(ass =>
                {
                    try { return ass.GetTypes(); }
                    catch { return Type.EmptyTypes; }
                })
                .Where(type => !type.IsAbstract && typeof(AbilityCondition).IsAssignableFrom(type))
                .ToDictionary(type => ObjectNames.NicifyVariableName(type.Name), type => type);

        private static string GetShortTypeName(string typeName)
        {
            if (typeName.IsNullOrEmpty()) return null;
            var parts = typeName.Split(' ');
            return parts.Length > 1 ? parts[1].Split('.').Last() : typeName;
        }
    }
}
#endif
