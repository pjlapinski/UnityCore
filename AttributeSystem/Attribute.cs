using System;
using System.Collections.Generic;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AttributeSystem
{
    [Serializable]
    public struct AttributeDefinition
    {
        [field: SerializeField] public GameplayTag IdentifierTag { get; set; }
        [field: SerializeField] public GameplayTagsContainer Tags { get; set; }
    }

    [Serializable]
    public struct Attribute
    {
        [field: SerializeField] public GameplayTag IdentifierTag { get; set; }
        [field: SerializeField] public GameplayTagsContainer Tags { get; set; }
        [field: SerializeField] public float BaseValue { get; set; }
        [field: SerializeField] public float Value { get; set; }

        public Attribute WithModifiers(IEnumerable<AttributeModifier> modifiers)
        {
            if (modifiers == null) return this;
            var addPre = 0f;
            var addPost = 0f;
            var mult = 1f;

            foreach (var mod in modifiers)
            {
                switch (mod.Type)
                {
                    case AttributeModifierType.AdditiveBase: addPre += mod.Value; break;
                    case AttributeModifierType.AdditiveFinal: addPost += mod.Value; break;
                    case AttributeModifierType.Multiplicative: mult *= mod.Value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            Value = (BaseValue + addPre) * mult + addPost;
            return this;
        }

        public Attribute CurrentToBase()
        {
            BaseValue = Value;
            return this;
        }
    }
}
