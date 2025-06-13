using System;
using System.Collections.Generic;
using System.Linq;
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
            attribute.UpdateCurrentValue(_modifiers[tag]);
            _attributes[tag] = attribute;
        }

        public float GetAttribute(GameplayTag tag) => _attributes[tag].CurrentValue;

        public float GetAttributeBase(GameplayTag tag) => _attributes[tag].BaseValue;

        public void AddModifier(GameplayTag tag, AttributeModifier modifier)
        {
            var attr = _attributes[tag];
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
            attr.UpdateCurrentValue(mods);
            _attributes[tag] = attr;
        }

        public void RemoveModifier(GameplayTag tag, GameplayTag modifier)
        {
            var attr = _attributes[tag];
            var mods = _modifiers[tag];
            var idx = mods.FindIndex(mod => mod.Tag == modifier);
            if (idx == -1) return;
            var mod = mods[idx];
            --mod.Sources;
            if (mod.Sources <= 0)
                mods.RemoveAt(idx);
            attr.UpdateCurrentValue(mods);
            _attributes[tag] = attr;
        }

        public void RemoveModifier(GameplayTag tag, AttributeModifier modifier) => RemoveModifier(tag, modifier.Tag);

        [Button]
        private void UpdateAllCurrentValues()
        {
            var newAttrs = new AssociativeArray<GameplayTag, Attribute>();
            foreach (var (tag, attr) in _attributes)
            {
                attr.UpdateCurrentValue(_modifiers[tag]);
                newAttrs[tag] = attr;
            }
            _attributes = newAttrs;
        }

        #endregion

        #region Initialization

        private void CopyAttributesFromSet()
        {
            if (_attributeValues == null)
            {
                ContextLogger.LogFormat(LogType.Warning, "ABILITY_SYSTEM", "No attribute values assigned for object {0}.", gameObject.name);
                return;
            }

            foreach (var data in _attributeValues._data)
            {
                _attributes[data._tag] = new()
                {
                    BaseValue = Mathf.Clamp(data._attribute, data._min, data._max),
                    CurrentValue = Mathf.Clamp(data._attribute, data._min, data._max),
                };
                _modifiers[data._tag] = new();
            }
        }

        private void Awake()
        {
            CopyAttributesFromSet();
        }

        #endregion
    }
}
