using System;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [Serializable]
    public enum ModifierType
    {
        AdditivePreMult, AdditivePostMult, Multiplicative, Override
    }

    [Serializable]
    public struct AttributeModifier
    {
        [field: SerializeField] public GameplayTag Tag { get; set; }
        [field: SerializeField] public ModifierType Type { get; set; }
        [field: SerializeField] public float Value { get; set; }
    }
}
