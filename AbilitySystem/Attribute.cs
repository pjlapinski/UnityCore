using System;
using System.Collections.Generic;
using PJL.Patterns;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [Serializable]
    public struct Attribute
    {
        [field: SerializeField] internal float BaseValue { get; set; }
        [field: SerializeField] internal float CurrentValue { get; set; }

        public void UpdateCurrentValue(IEnumerable<AttributeModifier> modifiers)
        {
            var add = 0f;
            var mul = 1f;
            var over = Option<float>.None;

            foreach (var mod in modifiers)
            {
                switch (mod.Type)
                {
                    case ModifierType.Additive:
                        add += mod.Value;
                        break;
                    case ModifierType.Multiplicative:
                        mul *= mod.Value;
                        break;
                    case ModifierType.Override:
                        over = mod.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (over.TryUnwrap(out var o))
                CurrentValue = o;
            else
                CurrentValue = (BaseValue + add) * mul;
        }
    }
}
