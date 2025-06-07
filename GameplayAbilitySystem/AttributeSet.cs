using PJL.GameplayTags;
using UnityEngine;

namespace PJL.GameplayAbilitySystem
{
    [CreateAssetMenu(menuName = "PJL/Gameplay Ability System/Attribute Set", fileName = "New Attribute Set")]
    public class AttributeSet : ScriptableObject
    {
        [SerializeField] private GameplayTag[] _attributes;
    }
}
