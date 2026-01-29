using System;
using NaughtyAttributes;
using PJL.Data;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [Serializable]
    public enum EffectType { Instantaneous, Infinite, Duration, }

    [Serializable]
    public abstract class AbilityEffect
    {
        /// The tag representing this effect
        [field: SerializeField, AllowNesting, Tooltip("The tag representing this effect")]
        public GameplayTag Tag { get; set; }
        /// The amount of stacks this effect applies
        [field: SerializeField, AllowNesting, Tooltip("The amount of stacks this effect applies")] 
        public int Stacks { get; set; } = 1;
        /// How many stacks should be added to the effect once the AbilitySystem ticks
        [field: SerializeField, AllowNesting, Tooltip("How many stacks should be added to the effect once the AbilitySystem ticks")] 
        public int ModifyStacksOnApply { get; set; }
        /// Determines how the effect is applied
        [field: SerializeField, AllowNesting, Tooltip("Determines how the effect is applied")] 
        public EffectType Type { get; set; }
        /// How long should the effect last. Only allowed if Type is Duration
#if UNITY_EDITOR
        [field: EnableIf(nameof(EnableDuration))]
#endif
        [field: SerializeField, AllowNesting, Tooltip("How long should the effect last. Only allowed if Type is Duration")] 
        public float Duration { get; set; }
        /// How often should this effect tick. Only allowed if type is not Instantaneous
#if UNITY_EDITOR
        [field: EnableIf(nameof(EnablePeriod))]
#endif
        [field: SerializeField, AllowNesting, Tooltip("How often should this effect tick. Only allowed if type is not Instantaneous")] 
        public float Period { get; set; }
        /// Should the effect be applied immediately after it's used, or should it wait for the next tick period. Only allowed if type is not Instantaneous
#if UNITY_EDITOR
        [field: EnableIf(nameof(EnableApplyInstantly))]
#endif
        [field: SerializeField, AllowNesting, Tooltip("Should the effect be applied immediately after it's used, or should it wait for the next tick period. Only allowed if type is not Instantaneous")] 
        public bool ApplyInstantly { get; set; }
        /// Should the effect also be applied when it's being removed
        [field: SerializeField, AllowNesting, Tooltip("Should the effect also be applied when it's being removed")] 
        public bool ApplyOnRemove { get; set; }
        /// Should the effect be applied when the period elapses while ticking. Only allowed if Type is not Instantaneous
#if UNITY_EDITOR
        [field: EnableIf(nameof(EnableApplyOnTick))]
#endif
        [field: SerializeField, AllowNesting, Tooltip("Should the effect be applied when the period elapses while ticking. Only allowed if Type is not Instantaneous")] 
        public bool ApplyOnTick { get; set; }

#if UNITY_EDITOR
        private bool EnableDuration => Type == EffectType.Duration;
        private bool EnablePeriod => Type != EffectType.Instantaneous;
        private bool EnableApplyInstantly => Type != EffectType.Instantaneous;
        private bool EnableApplyOnTick => Type != EffectType.Instantaneous;
#endif

        public virtual void Apply(IAbilityTarget target) {}
        public virtual void Remove(IAbilityTarget target) {}
    }

    [Serializable]
    internal class EffectTracker
    {
        [SerializeReference, TypeSelect] internal AbilityEffect _effect;
        [SerializeField] internal int _stacks;
        [SerializeField] internal float _periodTracker;
        [SerializeField] internal float _durationTracker;
        [SerializeField] internal bool _remove;

        internal bool ShouldApply => _periodTracker >= _effect.Period;
        internal bool ShouldRemove => _stacks <= 0 || (_effect.Type == EffectType.Duration && _durationTracker <= 0f);

        internal void Apply(IAbilityTarget target) => _effect.Apply(target);

        internal void Remove(IAbilityTarget target)
        {
            _effect.Remove(target);
            _remove = true;
        }
    }
}
