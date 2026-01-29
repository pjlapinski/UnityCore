#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PJL.Data.Editor
{
    [CustomPropertyDrawer(typeof(MonoRef<>))]
    [CustomPropertyDrawer(typeof(MonoRef<,>))]
    public class MonoRefDrawer : PropertyDrawer
    {
        private Type _interfaceType, _objectType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_interfaceType == null || _objectType == null) InitTypes();

            var prop = property.FindPropertyRelative("_object");

            EditorGUI.BeginProperty(position, label, property);
            var potential = EditorGUI.ObjectField(
                position, 
                label, 
                prop.objectReferenceValue, 
                typeof(Object), 
                true
            );
            var realObj = potential is GameObject go ? go.GetComponent(_interfaceType) : potential;

            if (realObj == null)
                prop.objectReferenceValue = null;
            else if (_interfaceType!.IsAssignableFrom(realObj.GetType()) && _objectType!.IsAssignableFrom(realObj.GetType()))
                prop.objectReferenceValue = realObj;
            else
                prop.objectReferenceValue = null;

            EditorGUI.EndProperty();
        }

        private void InitTypes()
        {
            var args = fieldInfo.FieldType.GenericTypeArguments;
            if (args.Length == 1)
            {
                _interfaceType = args[0];
                _objectType = typeof(Object);
            }
            else
            {
                _interfaceType = args[0];
                _objectType = args[1];
            }
        }
    }
}
#endif
