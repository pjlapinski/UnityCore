#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace PJL.Data.Editor
{
    [CustomPropertyDrawer(typeof(TypeSelectAttribute), true)]
    public class TypeSelectDrawer : PropertyDrawer
    {
        private Dictionary<string, Type> _inheritors;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_inheritors == null) 
                BuildInheritorsMap(TypeFromName(property.managedReferenceFieldTypename));

            var labelRect = new Rect(position.x, position.y, position.width / 2, EditorGUIUtility.singleLineHeight);
            var typeRect = new Rect(position.x + position.width / 2, position.y, position.width / 2, EditorGUIUtility.singleLineHeight);
            var dataRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, position.height - EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.LabelField(labelRect, label);

            var typeName = GetShortTypeName(property.managedReferenceFullTypename);
            if (EditorGUI.DropdownButton(typeRect, new GUIContent(typeName ?? "<null>"), FocusType.Keyboard))
            {
                var menu = new GenericMenu();
                foreach (var (name, type) in _inheritors)
                {
                    menu.AddItem(new GUIContent(name), type.FullName == typeName, () =>
                    {
                        property.managedReferenceValue = Activator.CreateInstance(type);
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }
                menu.ShowAsContext();
            }

            if (property.managedReferenceValue != null)
            {
                ++EditorGUI.indentLevel;
                EditorGUI.PropertyField(dataRect, property, new GUIContent("Value"), true);
                --EditorGUI.indentLevel;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, label) + (property.managedReferenceValue == null ? 0 : EditorGUIUtility.singleLineHeight);

        private void BuildInheritorsMap(Type baseType) =>
            _inheritors = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(ass =>
                {
                    try { return ass.GetTypes(); }
                    catch { return Type.EmptyTypes; }
                })
                .Where(type => !type.IsAbstract && baseType.IsAssignableFrom(type) && !typeof(UnityEngine.Object).IsAssignableFrom(type))
                .ToDictionary(type => ObjectNames.NicifyVariableName(type.Name), type => type);

        private static string GetShortTypeName(string typeName)
        {
            if (typeName.IsNullOrEmpty()) return null;
            var parts = typeName.Split(' ');
            return parts.Length > 1 ? parts[1].Split('.').Last() : typeName;
        }

        private static Type TypeFromName(string typeName)
        {
            if (typeName.IsNullOrEmpty()) return null;
            var parts = typeName.Split(' ');
            var assembly = Assembly.Load(parts[0]);
            return assembly.GetType(parts[1]);
        }
    }
}
#endif
