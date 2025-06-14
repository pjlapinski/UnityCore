using System;
using System.Collections.Generic;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [CreateAssetMenu(menuName = "PJL/Ability System/Ability", fileName = "New Ability")]
    public class Ability : ScriptableObject
    {
        [field: SerializeField] public GameplayTag Tag { get; set; }
        [field: SerializeReference] public List<AbilityEffect> TargetEffects { get; set; }
        [field: SerializeReference] public List<AbilityEffect> CasterEffects { get; set; }

        [field: SerializeReference] public List<AbilityCondition> Conditions { get; set; }
        [field: SerializeField] public float Cooldown { get; set; }
        [field: SerializeField] public bool StartsOnCooldown { get; set; }

        internal void Execute(AbilitySystem caster, IAbilityTarget target)
        {
            foreach (var effect in TargetEffects)
                target.AddEffect(effect);
            foreach (var effect in CasterEffects)
                caster.AddEffect(effect);
        }
    }

    [Serializable]
    internal class AbilityTracker
    {
        [SerializeField] internal Ability _ability;
        [SerializeField] internal float _cooldownTracker, _cooldown;

        internal float BaseCooldown => _ability.Cooldown;

        internal bool CanExecute(AbilitySystem caster)
        {
            if (_cooldownTracker > 0f) return false;
            foreach (var condition in _ability.Conditions)
                if (!condition.Check(caster))
                    return false;
            return true;
        }

        internal void Execute(AbilitySystem caster, IAbilityTarget target)
        {
            if (!CanExecute(caster)) return;
            _ability.Execute(caster, target);
        }

        internal void PutOnCooldown() => _cooldownTracker = _ability.Cooldown;
        internal void PutOffCooldown() => _cooldownTracker = 0f;

        internal void SetCooldown(float newCd) => _cooldown = newCd;

        internal void ResetCooldown() => SetCooldown(BaseCooldown);

    }
}
