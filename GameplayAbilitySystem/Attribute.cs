using System;
using UnityEngine;

namespace PJL.GameplayAbilitySystem
{
    [Serializable]
    public struct Attribute
    {
        [field: SerializeField] public float BaseValue { get; set; }
        [field: SerializeField] public float CurrentValue { get; set; }
    }
}
