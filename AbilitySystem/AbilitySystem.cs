using System.Collections.Generic;
using NaughtyAttributes;
using PJL.Collections;
using PJL.Debug;
using PJL.GameplayTags;
using UnityEngine;
using UnityEngine.Events;

namespace PJL.AbilitySystem
{
    public class AbilitySystem : MonoBehaviour, IAbilityTarget
    {
        [SerializeField] private AttributeValues _attributeValues;
        [SerializeField] private HashMap<GameplayTag, AttributeTracker> _attributes;
        [SerializeField] private GameplayTagsContainer _tags;
        [SerializeField] private List<EffectTracker> _effects;
        [SerializeField] private HashMap<GameplayTag, AbilityTracker> _abilities;

        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag, float> OnAttributeChanged { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag> OnTagAdded { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag> OnTagRemoved { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag> OnEffectApplied { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag> OnEffectRemoved { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag> OnAbilityExecuted { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag> OnAbilityAdded { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag> OnAbilityRemoved { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag> OnAbilityPutOnCooldown { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag> OnAbilityPutOffCooldown { get; set; }
        [field: Foldout("Events")]
        [field: SerializeField] public UnityEvent<GameplayTag, float> OnAbilityCooldownChanged { get; set; }

        public void Tick(float delta)
        {
            TickAbilityCooldowns(delta);
            TickEffects(delta);
        }

        #region Attributes

        public bool HasAttribute(GameplayTag attribute) =>
            _attributes.ContainsKey(attribute);

        public void SetAttributeBase(GameplayTag attribute, float value)
        {
            if (_attributes.TryGetValue(attribute, out var attr))
            {
                attr.BaseValue = value;
                OnAttributeChanged.Invoke(attribute, attr.CurrentValue);
            }
        }

        public float GetAttribute(GameplayTag attribute) =>
            _attributes.TryGetValue(attribute, out var attr) ? attr.CurrentValue : 0f;

        public float GetAttributeBase(GameplayTag attribute) =>
            _attributes.TryGetValue(attribute, out var attr) ? attr.BaseValue : 0f;

        public void AddAttributeModifier(GameplayTag attribute, AttributeModifier modifier)
        {
            if (_attributes.TryGetValue(attribute, out var attr))
            {
                attr.AddModifier(modifier);
                OnAttributeChanged.Invoke(attribute, attr.CurrentValue);
            }
        }

        public void RemoveAttributeModifier(GameplayTag attribute, GameplayTag modifier)
        {
            if (_attributes.TryGetValue(attribute, out var attr))
            {
                attr.RemoveModifier(modifier);
                OnAttributeChanged.Invoke(attribute, attr.CurrentValue);
            }
        }

        public void RemoveAttributeModifier(GameplayTag attribute, AttributeModifier modifier) => RemoveAttributeModifier(attribute, modifier.Tag);

        public void ApplyAttributeModifiers(GameplayTag attribute)
        {
            _attributes.TryGetValue(attribute, out var attr);
            attr._attribute.BaseValue = attr.CurrentValue;
            attr._modifiers.Clear();
            OnAttributeChanged.Invoke(attribute, attr.CurrentValue);
        }

        #endregion

        #region Effects

        public bool HasTag(GameplayTag tag) => _tags.Contains(tag);
        public bool HasTagExact(GameplayTag tag) => _tags.ContainsExact(tag);
        public void AddTag(GameplayTag tag)
        {
            _tags.AddTag(tag);
            OnTagAdded.Invoke(tag);
        }

        public void RemoveTag(GameplayTag tag)
        {
            _tags.Remove(tag);
            OnTagRemoved.Invoke(tag);
        }

        public void TickEffects(float value)
        {
            foreach (var effect in _effects)
            {
                if (effect._effect.Type == EffectType.Duration) effect._durationTracker -= value;
                effect._periodTracker += value;
                if (effect.ShouldApply)
                {
                    effect._stacks += effect._effect.ModifyStacksOnApply;
                    if (effect._effect.ApplyOnTick)
                        effect.Apply(this);
                    effect._periodTracker = 0f;
                    OnEffectApplied.Invoke(effect._effect.Tag);
                }

                if (effect.ShouldRemove)
                {
                    effect.Remove(this);
                    OnEffectRemoved.Invoke(effect._effect.Tag);
                }
            }

            _effects.RemoveAll(e => e._remove);
        }

        public void AddEffect(AbilityEffect effect)
        {
            if (effect.Type == EffectType.Instantaneous)
            {
                effect.Apply(this);
                OnEffectApplied.Invoke(effect.Tag);
                effect.Remove(this);
                OnEffectRemoved.Invoke(effect.Tag);
                return;
            }
            _effects.Add(new EffectTracker
            {
                _effect = effect,
                _stacks = effect.Stacks,
                _periodTracker = 0f,
                _durationTracker = effect.Duration,
            });

            if (effect.ApplyInstantly)
            {
                effect.Apply(this);
                OnEffectApplied.Invoke(effect.Tag);
            }
        }

        public void RemoveEffect(AbilityEffect effect) => RemoveEffect(effect.Tag);
        public void RemoveEffect(GameplayTag tag)
        {
            var idx = _effects.FindIndex(e => e._effect.Tag == tag);
            if (idx == -1) return;
            var effect = _effects[idx];
            if (effect._remove) return;
            if (effect._effect.ApplyOnRemove)
            {
                effect.Apply(this);
                OnEffectApplied.Invoke(effect._effect.Tag);
            }

            effect.Remove(this);
            OnEffectRemoved.Invoke(effect._effect.Tag);
        }

        public bool HasEffect(GameplayTag tag)
        {
            foreach (var e in _effects)
                if (e._effect.Tag == tag) return true;
            return false;
        }

        #endregion

        #region Abilities

        public void AddAbility(Ability ability)
        {
            if (_abilities.ContainsKey(ability.Tag)) return;
            _abilities[ability.Tag] = new()
            {
                _ability = ability,
                _cooldown = ability.Cooldown,
                _cooldownTracker = ability.StartsOnCooldown ? ability.Cooldown : 0f
            };
            OnAbilityAdded.Invoke(ability.Tag);
        }

        public void RemoveAbility(GameplayTag ability)
        {
            _abilities.Remove(ability);
            OnAbilityRemoved.Invoke(ability);
        }

        public void TickAbilityCooldowns(float value)
        {
            foreach (var (tag, ability) in _abilities)
            {
                if (ability._cooldownTracker <= 0f) continue;
                ability._cooldownTracker -= value;
                if (ability._cooldownTracker <= 0f)
                    OnAbilityPutOffCooldown.Invoke(tag);
            }
        }

        public float GetAbilityCooldown(GameplayTag ability) =>
            _abilities.TryGetValue(ability, out var a) ? a._cooldown : 0f;

        public float GetAbilityBaseCooldown(GameplayTag ability) =>
            _abilities.TryGetValue(ability, out var a) ? a.BaseCooldown : float.PositiveInfinity;

        public bool CanExecuteAbility(GameplayTag ability) =>
            _abilities.TryGetValue(ability, out var a) && a.CanExecute(this);

        public void ExecuteAbility(GameplayTag ability, IEnumerable<IAbilityTarget> targets)
        {
            if (_abilities.TryGetValue(ability, out var a))
            {
                a.Execute(this, targets);
                OnAbilityExecuted.Invoke(ability);
            }
        }

        public void PutAbilityOnCooldown(GameplayTag ability)
        {
            if (_abilities.TryGetValue(ability, out var a))
            {
                a.PutOnCooldown();
                OnAbilityPutOnCooldown.Invoke(ability);
            }
        }

        public void PutAbilityOffCooldown(GameplayTag ability)
        {
            if (_abilities.TryGetValue(ability, out var a))
            {
                a.PutOffCooldown();
                OnAbilityPutOffCooldown.Invoke(ability);
            }
        }

        public void SetAbilityCooldown(GameplayTag ability, float newCd)
        {
            if (_abilities.TryGetValue(ability, out var a))
            {
                a.SetCooldown(newCd);
                OnAbilityCooldownChanged.Invoke(ability, newCd);
            }
        }

        public void ResetAbilityCooldown(GameplayTag ability)
        {
            if (_abilities.TryGetValue(ability, out var a))
            {
                a.ResetCooldown();
                OnAbilityCooldownChanged.Invoke(ability, a.BaseCooldown);
            }
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
                _attributes.Add(
                    data._tag,
                    new AttributeTracker
                    {
                        _attribute = new Attribute
                        {
                            BaseValue = Mathf.Clamp(data._attribute, data._min, data._max),
                            CurrentValue = Mathf.Clamp(data._attribute, data._min, data._max),
                        },
                        _modifiers = new()
                    });
        }

        private void Awake()
        {
            CopyAttributesFromSet();
        }

        #endregion
    }
}
