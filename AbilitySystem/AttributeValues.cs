using System;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [Serializable]
    internal struct TagAttributeData
    {
        [SerializeField] internal GameplayTag _tag;
        [SerializeField] internal float _initialValue, _min, _max;
    }

    [CreateAssetMenu(fileName = "New Attribute Values", menuName = "PJL/Ability System/Attribute Values")]
    public class AttributeValues : ScriptableObject
    {
        [SerializeReference] private AttributeSet _attributeSet;
        [SerializeField] internal TagAttributeData[] _data;
    }
}
