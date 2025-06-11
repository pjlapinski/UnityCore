using System;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [Serializable]
    internal struct TagAttributePair
    {
        [SerializeField] internal GameplayTag Tag;
        [SerializeField] internal Attribute Attribute;
    }

    [CreateAssetMenu(fileName = "New Attribute Values", menuName = "PJL/Ability System/Attribute Values")]
    public class AttributeValues : ScriptableObject
    {
        [SerializeReference] private AttributeSet _attributeSet;
        [SerializeField] internal TagAttributePair[] _data;
    }
}
