using UnityEngine;

namespace PJL.Data
{
    [CreateAssetMenu(fileName = "Const String List",  menuName = "PJL/ConstStringList")]
    public class ConstStringList : ScriptableObject
    {
        [field: SerializeField] internal string Namespace { get; private set; }
        [field: SerializeField] internal string GroupName { get; private set; }
        [field: SerializeField] internal string[] Values { get; private set; }
    }
}
