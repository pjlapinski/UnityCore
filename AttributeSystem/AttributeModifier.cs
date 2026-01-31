using System;
using PJL.GameplayTags;
using PJL.Utilities.Extensions;
using UnityEngine;

namespace PJL.AttributeSystem
{
    [Serializable]
    public enum AttributeModifierType { AdditiveBase, AdditiveFinal, Multiplicative }

    [Serializable]
    public struct AttributeModifier
    {
        [field: SerializeField] public GameplayTag IdentifierTag { get; set; }
        [field: SerializeField] public GameplayTagsContainer Tags { get; set; }
        [field: SerializeField] public AttributeModifierType Type { get; set; }
        [field: SerializeField] public float Value { get; set; }

        public AttributeModifier Combine(AttributeModifier other) => new() 
        {
            IdentifierTag = IdentifierTag,
            Tags = Tags,
            Type = Type,
            Value = Type switch
            {
                AttributeModifierType.AdditiveBase => Value + other.Value,
                AttributeModifierType.AdditiveFinal => Value + other.Value,
                AttributeModifierType.Multiplicative => Value * other.Value,
                _ => 0 
            }
        };

        public AttributeModifier Separate(AttributeModifier other) => new()
        {
            IdentifierTag = IdentifierTag,
            Tags = Tags,
            Type = Type,
            Value = Type switch
            {
                AttributeModifierType.AdditiveBase => Value - other.Value,
                AttributeModifierType.AdditiveFinal => Value - other.Value,
                AttributeModifierType.Multiplicative => Value / other.Value,
                _ => 0
            }
        };

        public bool IsZero => Type switch
        {
            AttributeModifierType.AdditiveBase => Value.AlmostEquals(0),
            AttributeModifierType.AdditiveFinal => Value.AlmostEquals(0),
            AttributeModifierType.Multiplicative => Value.AlmostEquals(1),
            _ => true
        };
    }
}
