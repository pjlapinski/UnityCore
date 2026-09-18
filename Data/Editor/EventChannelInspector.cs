#if UNITY_EDITOR
using System;
using System.Collections;
using System.Reflection;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PJL.Data.Editor
{
    [CanEditMultipleObjects, CustomEditor(typeof(EventChannel), true)]
    public class EventChannelInspector : UnityEditor.Editor
    {
        private EventChannel Target => (EventChannel)serializedObject.targetObject;
        private SerializedProperty _argTypes, _event;
        private string[] _subscribers;

        private void OnEnable()
        {
            _argTypes = serializedObject.FindProperty("_argumentTypes");
            _event = serializedObject.FindProperty("_event");
        }

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                GetSubscribers();
                _subscribers.Visit(sub => EditorGUILayout.LabelField(sub));
                return;
            }

            if (_argTypes.arraySize > 4) 
                _argTypes.arraySize = 4;

            EditorGUILayout.PropertyField(_argTypes);
            UpdateEventObject();

            serializedObject.ApplyModifiedProperties();
        }

        private void GetSubscribers()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            _subscribers ??= Array.Empty<string>();

            var targetField = typeof(EventChannel).GetField("_event", flags);
            var target = targetField.GetValue(Target);

            var callsField = target.GetType().GetField("m_PersistentCalls", flags);
            var calls = callsField.GetValue(target);
            var callsInternal = calls.GetType().GetField("m_Calls", flags);
            var callsArr = (IEnumerable)callsInternal.GetValue(calls);
            var callsLen = 0;
            foreach (var _ in callsArr) ++callsLen;

            if (_subscribers.Length != callsLen)
                Array.Resize(ref _subscribers, callsLen);

            var i = 0;
            foreach (var el in callsArr)
            {
                var elType = el.GetType();
                var caller = elType.GetField("m_Target", flags)?.GetValue(el);
                var method = (string)elType.GetField("m_MethodName", flags)?.GetValue(el);
                if (caller == null) continue;

                if (caller is UnityEngine.Object u)
                    _subscribers[i] = $"{u.name} ({method})";
                else
                    _subscribers[i] = $"{caller.GetType().Name} ({method})";
                ++i;
            }
        }

        private void UpdateEventObject()
        {
            var eventT = _event.managedReferenceValue?.GetType() ?? typeof(object);
            var targetType = _argTypes.arraySize switch
            {
                0 => typeof(UnityEvent),
                1 => typeof(UnityEvent<>).MakeGenericType(
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(0))
                ),
                2 => typeof(UnityEvent<,>).MakeGenericType(
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(0)),
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(1))
                ),
                3 => typeof(UnityEvent<,,>).MakeGenericType(
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(0)),
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(1)),
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(2))
                ),
                4 => typeof(UnityEvent<,,,>).MakeGenericType(
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(0)),
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(1)),
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(2)),
                    ArgTypeToType(_argTypes.GetArrayElementAtIndex(3))
                ),
                _ => throw new ArgumentException("unreachable")
            };

            if (eventT != targetType)
                _event.managedReferenceValue = Activator.CreateInstance(targetType);
        }

        private Type ArgTypeToType(SerializedProperty p) => (ArgumentType)p.enumValueIndex switch
        {
            ArgumentType.Bool => typeof(bool),
            ArgumentType.Color => typeof(Color),
            ArgumentType.Double => typeof(double),
            ArgumentType.Float => typeof(float),
            ArgumentType.Int => typeof(int),
            ArgumentType.Uint => typeof(uint),
            ArgumentType.Long => typeof(long),
            ArgumentType.Ulong => typeof(ulong),
            ArgumentType.String => typeof(string),
            ArgumentType.Guid => typeof(Guid),
            ArgumentType.Enumerable => typeof(IEnumerable),
            ArgumentType.Vector2 => typeof(Vector2),
            ArgumentType.Vector2Int => typeof(Vector2Int),
            ArgumentType.Vector3 => typeof(Vector3),
            ArgumentType.Vector3Int => typeof(Vector3Int),
            ArgumentType.Vector4 => typeof(Vector4),
            ArgumentType.Quaternion => typeof(Quaternion),
            ArgumentType.Transform => typeof(Transform),
            ArgumentType.UnityObject => typeof(UnityEngine.Object),
            ArgumentType.Object => typeof(System.Object),
            _ => throw new ArgumentOutOfRangeException(p.name)
        };
    }
}
#endif
