using System.Linq;
using System.Reflection;
using PJL.Data;
using UnityEditor;

#if UNITY_EDITOR
namespace PJL.EditorAttributes.Editor
{
    internal static class ReflectionUtility
    {
        internal const BindingFlags SearchFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        internal static bool ResolveCondition(string name, SerializedProperty prop) => 
            GetFieldValue<bool>(name, prop).TryUnwrap(out var b) && b;

        internal static Option<T> GetFieldValue<T>(string name, SerializedProperty prop)
        {
            var parentPath = string.Join('.', prop.propertyPath.Split('.').SkipLast(1));
            var parentVal = prop.serializedObject.FindProperty(parentPath)?.boxedValue ?? prop.serializedObject.targetObject;

            var t = parentVal.GetType();

            var f = t.GetField(name, SearchFlags);
            if (f != null && f.GetValue(parentVal) is T ret1)
                return ret1;
            var p = t.GetProperty(name, SearchFlags);
            if (p != null && p.GetValue(parentVal) is T ret2)
                return ret2;
            return Option<T>.None;
        }
    }
}
#endif
