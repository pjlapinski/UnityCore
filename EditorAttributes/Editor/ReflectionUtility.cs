using System.Linq;
using System.Reflection;
using UnityEditor;

#if UNITY_EDITOR
namespace PJL.EditorAttributes.Editor
{
    public static class ReflectionUtility
    {
        public static bool ResolveCondition(string name, SerializedProperty prop)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

            var parentPath = string.Join('.', prop.propertyPath.Split('.').SkipLast(1));
            var parentVal = prop.serializedObject.FindProperty(parentPath)?.boxedValue ?? prop.serializedObject.targetObject;

            var t = parentVal.GetType();

            var f = t.GetField(name, flags);
            if (f != null && f.GetValue(parentVal) is bool b1)
                return b1;
            var p = t.GetProperty(name, flags);
            if (p != null && p.GetValue(parentVal) is bool b2)
                return b2;
            return false;
        }
    }
}
#endif
