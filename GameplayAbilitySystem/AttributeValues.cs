using System.Collections.Generic;
using UnityEngine;

namespace PJL.GameplayAbilitySystem
{
    [CreateAssetMenu(menuName = "PJL/Gameplay Ability System/Attribute Values", fileName = "New Attribute Values")]
    public class AttributeValues : ScriptableObject
    {
        [SerializeReference] private AttributeSet _attributeSet;
        [SerializeField] private List<TagAttributePair> _data;
    }
}
