using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using PJL.Collections;
using PJL.Debug;
using PJL.GameplayTags;
using PJL.Patterns;
using PJL.Utilities;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class AbilitySystem : MonoBehaviour
    {
        [BoxGroup("Attributes")]
        [SerializeField] private AttributeValues _attributeValues;

        [BoxGroup("Attributes"), Indent]
        [SerializeField] private AssociativeArray<GameplayTag, Attribute> _attributes;

        [BoxGroup("Attributes"), Indent]
        [SerializeField] private AssociativeArray<GameplayTag, List<AttributeModifier>> _modifiers;

        #region Attributes

        public void SetAttributeBase(GameplayTag attribute, float value)
        {
            if (!_attributes.TryGetValue(attribute, out var att)) return;
            att.BaseValue = value;
            att.UpdateCurrentValue(_modifiers[attribute]);
            _attributes[attribute] = att;
        }

        public float GetAttribute(GameplayTag attribute) => _attributes[attribute].CurrentValue;

        public float GetAttributeBase(GameplayTag attribute) => _attributes[attribute].BaseValue;

        public void AddModifier(GameplayTag attribute, AttributeModifier modifier)
        {
            var attr = _attributes[attribute];
            var mods = _modifiers[attribute];
            mods.Add(modifier);
            attr.UpdateCurrentValue(mods);
            _attributes[attribute] = attr;
        }

        public void RemoveModifier(GameplayTag attribute, GameplayTag modifier)
        {
            var attr = _attributes[attribute];
            var mods = _modifiers[attribute];
            var idx = mods.FindIndex(mod => mod.Tag == modifier);
            if (idx == -1) return;
            mods.RemoveAt(idx);
            attr.UpdateCurrentValue(mods);
            _attributes[attribute] = attr;
        }

        public void RemoveModifier(GameplayTag attribute, AttributeModifier modifier) => RemoveModifier(attribute, modifier.Tag);

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
