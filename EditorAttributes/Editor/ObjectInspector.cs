#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace PJL.EditorAttributes.Editor
{
    [CanEditMultipleObjects, CustomEditor(typeof(Object), true)]
    public class ObjectInspector : UnityEditor.Editor
    {
        private IEnumerable<(MethodInfo method, ButtonAttribute attribute)> _methods;

        public override void OnInspectorGUI()
        {
            if (_methods == null) FindMethodsWithButtons();

            DrawDefaultInspector();
            DrawMethodButtons();
        }

        private void DrawMethodButtons()
        {
            foreach (var (method, attr) in _methods)
            {
                var mName = attr.Text.IsNullOrWhiteSpace() ? ObjectNames.NicifyVariableName(method.Name) : attr.Text;
                if (GUILayout.Button(mName))
                {
                    var ps = method.GetParameters().Select(p => p.DefaultValue).ToArray();
                    method.Invoke(method.IsStatic ? null : serializedObject.targetObject, ps);
                }
            }
        }

        private void FindMethodsWithButtons()
        {
            var o = serializedObject.targetObject;
            var t =  o.GetType();

            _methods = t.GetMethodsWithAttribute<ButtonAttribute>(ReflectionUtility.SearchFlags);
        }
    }
}
#endif
