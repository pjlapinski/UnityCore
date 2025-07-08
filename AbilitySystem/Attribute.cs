﻿using System;
using System.Collections.Generic;
using PJL.GameplayTags;
using PJL.Patterns;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace PJL.AbilitySystem
{
    [Serializable]
    public struct Attribute
    {
        [field: SerializeField] public float BaseValue { get; set; }
        [field: SerializeField] public float CurrentValue { get; set; }

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

    [Serializable]
    internal class AttributeTracker
    {
        [SerializeField] internal Attribute _attribute;
        [SerializeField] internal List<AttributeModifier> _modifiers;

        public float BaseValue
        {
            get => _attribute.BaseValue;
            set
            {
                var attr = _attribute;
                attr.BaseValue = value;
                attr.UpdateCurrentValue(_modifiers);
                _attribute = attr;
            }
        }

        public float CurrentValue => _attribute.CurrentValue;

        public void AddModifier(AttributeModifier modifier)
        {
            _modifiers.Add(modifier);
            var attr = _attribute;
            attr.UpdateCurrentValue(_modifiers);
            _attribute = attr;
        }

        public void RemoveModifier(GameplayTag tag)
        {
            var idx = _modifiers.FindIndex(mod => mod.Tag == tag);
            if (idx == -1) return;
            _modifiers.RemoveAt(idx);
            var attr = _attribute;
            attr.UpdateCurrentValue(_modifiers);
            _attribute = attr;
        }
    }
}
