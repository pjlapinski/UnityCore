using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [CreateAssetMenu(fileName = "New Attribute Set", menuName = "PJL/Ability System/Attribute Set")]
    public class AttributeSet : ScriptableObject
    {
        [SerializeField] private GameplayTag[] _attributes;
    }
}
