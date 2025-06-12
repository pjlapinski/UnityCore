using System;
using System.Collections.Generic;
using NaughtyAttributes;
using PJL.Collections;
using PJL.Debug;
using PJL.GameplayTags;
using PJL.Patterns;
using PJL.Utilities;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class AbilitySystem : MonoBehaviour
    {
        [BoxGroup("Attributes")]
        [SerializeField] private AttributeValues _attributeValues;

        [BoxGroup("Attributes"), DisableIf(nameof(FieldsReadOnly)), Indent]
        [SerializeField] private AssociativeArray<GameplayTag, Attribute> _attributes;

        [BoxGroup("Attributes"), DisableIf(nameof(FieldsReadOnly)), Indent]
        [SerializeField] private AssociativeArray<GameplayTag, List<AttributeModifier>> _modifiers;

        private bool FieldsReadOnly => !Application.isPlaying;

        #region Attributes

        public void SetAttributeBase(GameplayTag tag, float value)
        {
            if (!_attributes.TryGetValue(tag, out var attribute)) return;
            attribute.BaseValue = value;
            _attributes[tag] = attribute;
        }

        public float GetAttribute(GameplayTag tag)
        {
            var mods = _modifiers[tag];
            var add = 0f;
            var mul = 1f;
            var over = Option<float>.None;

            foreach (var mod in mods)
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
                return o;
            return (_attributes[tag].BaseValue + add) * mul;
        }

        public float GetAttributeBase(GameplayTag tag) => _attributes[tag].BaseValue;

        public void AddModifier(GameplayTag tag, AttributeModifier modifier)
        {
            var mods = _modifiers[tag];
            var idx = mods.FindIndex(mod => mod.Tag == modifier.Tag);
            if (idx == -1)
            {
                mods.Add(modifier);
                return;
            }
            var mod = mods[idx];
            ++mod.Sources;
            mod.Value = modifier.Value;
        }

        public void RemoveModifier(GameplayTag tag, GameplayTag modifier)
        {
            var mods = _modifiers[tag];
            var idx = mods.FindIndex(mod => mod.Tag == modifier);
            if (idx == -1) return;
            var mod = mods[idx];
            --mod.Sources;
            if (mod.Sources <= 0)
                mods.RemoveAt(idx);
        }

        public void RemoveModifier(GameplayTag tag, AttributeModifier modifier) => RemoveModifier(tag, modifier.Tag);

        #endregion

        #region Initialization

        private void CopyAttributesFromSet()
        {
            if (_attributeValues == null)
            {
                ContextLogger.LogFormat(LogType.Warning, "ABILITY_SYSTEM", "No attribute values assigned for object {0}.", gameObject.name);
                return;
            }

            foreach (var pair in _attributeValues._data)
            {
                _attributes[pair.Tag] = pair.Attribute;
                _modifiers[pair.Tag] = new();
            }
        }

        private void Awake()
        {
            CopyAttributesFromSet();
        }

        #endregion
    }
}
