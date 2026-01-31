using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using PJL.Collections;
using PJL.GameplayTags;
using UnityEngine;
using UnityEngine.Events;

namespace PJL.AttributeSystem
{
    public class AttributeContainer : MonoBehaviour
    {
        [SerializeField] private AttributeSet _attributeSet;
        [SerializeField] private AttributeSetValues _initialValues;
        [SerializeField, HideInInspector] private HashMap<GameplayTag, Attribute> _attributes;
        [SerializeField, HideInInspector] private HashMap<GameplayTag, List<AttributeModifier>> _modifiers;

        [field: SerializeField, Foldout("Events")] public UnityEvent<Attribute> OnAttributeModified { get; set; }
        [field: SerializeField, Foldout("Events")] public UnityEvent<Attribute, GameplayTag> OnModifierAdded { get; set; }
        [field: SerializeField, Foldout("Events")] public UnityEvent<Attribute, GameplayTag> OnModifierRemoved { get; set; }

        public IReadOnlyDictionary<GameplayTag, Attribute> Attributes => _attributes.Dictionary;
        public IReadOnlyDictionary<GameplayTag, IReadOnlyList<AttributeModifier>> Modifiers => _modifiers
            .Dictionary
            .ToDictionary(kvp => kvp.Key, kvp => (IReadOnlyList<AttributeModifier>)kvp.Value);

        public bool Has(GameplayTag attr) => _attributes.ContainsKey(attr);

        public float Get(GameplayTag attr) => _attributes.TryGetValue(attr, out var a) ? a.Value : 0f;

        public void Set(GameplayTag attr, float value)
        {
            if (!_attributes.TryGetValue(attr, out var a)) return;
            a.BaseValue = value;
            _attributes[attr] = a.WithModifiers(_modifiers[a.IdentifierTag]);
            OnAttributeModified?.Invoke(_attributes[attr]);
        }

        public void CurrentToBase(GameplayTag attr)
        {
            if (!_attributes.TryGetValue(attr, out var a)) return;
            a.BaseValue = a.Value;
            foreach (var mod in _modifiers[a.IdentifierTag])
                OnModifierRemoved?.Invoke(a, mod.IdentifierTag);
            _modifiers[a.IdentifierTag].Clear();
            _attributes[attr] = a;
        }

        public void AddModifier(GameplayTag attr, AttributeModifier modifier)
        {
            var idx = _modifiers[attr].FindIndex(mod => mod.IdentifierTag.MatchesTag(modifier.IdentifierTag));
            if (idx == -1) _modifiers[attr].Add(modifier);
            else _modifiers[attr][idx] = _modifiers[attr][idx].Combine(modifier);
            _attributes[attr] = _attributes[attr].WithModifiers(_modifiers[attr]);
            OnModifierAdded?.Invoke(_attributes[attr], modifier.IdentifierTag);
            OnAttributeModified?.Invoke(_attributes[attr]);
        }

        public void RemoveModifier(GameplayTag attr, AttributeModifier modifier)
        {
            var idx = _modifiers[attr].FindIndex(mod => mod.IdentifierTag.MatchesTag(modifier.IdentifierTag));
            if (idx == -1) return;
            _modifiers[attr][idx] = _modifiers[attr][idx].Separate(modifier);
            if (_modifiers[attr][idx].IsZero) _modifiers[attr].RemoveAt(idx);
            _attributes[attr] = _attributes[attr].WithModifiers(_modifiers[attr]);
            OnModifierRemoved?.Invoke(_attributes[attr], modifier.IdentifierTag);
            OnAttributeModified?.Invoke(_attributes[attr]);
        }

        public bool HasModifier(GameplayTag attr, GameplayTag modifier)
        {
            var idx = _modifiers[attr].FindIndex(mod => mod.IdentifierTag.MatchesTag(modifier));
            return idx >= 0;
        }

        public bool TryGetModifier(GameplayTag attr, GameplayTag modifier, out AttributeModifier mod)
        {
            var idx = _modifiers[attr].FindIndex(mod => mod.IdentifierTag.MatchesTag(modifier));
            mod = idx == -1 ? default : _modifiers[attr][idx];
            return idx >= 0;
        }

        public void SetValues(AttributeSetValues values)
        {
            foreach (var (attr, value) in values.Values)
            {
                var a = _attributes[attr];
                a.BaseValue = value;
                _attributes[attr] = a.WithModifiers(_modifiers[attr]);
                OnAttributeModified?.Invoke(_attributes[attr]);
            }
        }

        private void Awake()
        {
            _attributes ??= new();
            _modifiers ??= new();

            foreach (var attr in _attributeSet.Attributes)
            {
                _attributes[attr.IdentifierTag] = new Attribute
                {
                    IdentifierTag = attr.IdentifierTag,
                    Tags = attr.Tags.Copy(),
                    BaseValue = 0f,
                    Value = 0f
                };
                _modifiers[attr.IdentifierTag] = new();
            }

            if (_initialValues != null) SetValues(_initialValues);
        }
    }
}
