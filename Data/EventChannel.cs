using UnityEngine;
using UnityEngine.Events;

namespace PJL.Data
{
    internal enum ArgumentType
    {
        Bool,
        Color,
        Double,
        Float,
        Int,
        Uint,
        Long,
        Ulong,
        String,
        Guid,
        Enumerable,
        Vector2,
        Vector2Int,
        Vector3,
        Vector3Int,
        Vector4,
        Quaternion,
        Transform,
        UnityObject,
        Object
    }

    [CreateAssetMenu(fileName = "Event Channel", menuName = "PJL/Event Channel")]
    public class EventChannel : ScriptableObject
    {
        [SerializeField] private ArgumentType[] _argumentTypes;
        [SerializeReference] private UnityEventBase _event;
    }
}
