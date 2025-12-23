using UnityEngine;

namespace PJL.AbilitySystem
{
    [CreateAssetMenu(fileName = "New Ability List", menuName = "PJL/Ability System/Ability List")]
    public class AbilityList : ScriptableObject
    {
        [field: SerializeField] public Ability[] Abilities { get; set; }
    }
}
