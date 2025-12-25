using System;
using System.Runtime.InteropServices;
using NaughtyAttributes;
using PJL.Data;
using PJL.GameplayTags;
using UnityEngine;
using UnityEngine.Serialization;

namespace PJL.AbilitySystem
{
    [Serializable]
    public enum EffectType { Instantaneous, Infinite, Duration, }

    [Serializable]
    public abstract class AbilityEffect
    {
        [field: SerializeField] public GameplayTag Tag { get; set; }
        [field: SerializeField] public int Stacks { get; set; } = 1;
        [field: SerializeField] public int ModifyStacksOnApply { get; set; }
        [field: SerializeField] public EffectType Type { get; set; }
        [field: SerializeField] public float Duration { get; set; }
        [field: SerializeField] public float Period { get; set; }
        [field: SerializeField] public bool ApplyInstantly { get; set; }
        [field: SerializeField] public bool ApplyOnRemove { get; set; }
        [field: SerializeField] public bool ApplyOnTick { get; set; }

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
