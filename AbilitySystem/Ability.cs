using System;
using System.Collections.Generic;
using PJL.Data;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [CreateAssetMenu(menuName = "PJL/Ability System/Ability", fileName = "New Ability")]
    public class Ability : ScriptableObject
    {
        [field: SerializeField] public GameplayTag Tag { get; set; }
        [field: SerializeReference, TypeSelect] public List<AbilityEffect> TargetEffects { get; set; }
        [field: SerializeReference, TypeSelect] public List<AbilityEffect> CasterEffects { get; set; }

        [field: SerializeReference, TypeSelect] public List<AbilityCondition> Conditions { get; set; }
        [field: SerializeField] public float Cooldown { get; set; }
        [field: SerializeField] public bool StartsOnCooldown { get; set; }

        internal void Execute(AbilitySystem caster, IEnumerable<IAbilityTarget> targets)
        {
            foreach (var effect in CasterEffects)
                caster.AddEffect(effect, caster.Id);
            foreach (var target in targets)
                foreach (var effect in TargetEffects)
                    target.AddEffect(effect, caster.Id);
        }
    }

    [Serializable]
    internal class AbilityTracker
    {
        [SerializeField] internal Uuid _casterId;
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

        internal void Execute(AbilitySystem caster, IEnumerable<IAbilityTarget> targets)
        {
            if (!CanExecute(caster)) return;
            _casterId = caster.Id;
            _ability.Execute(caster, targets);
        }

        internal void PutOnCooldown() => _cooldownTracker = _cooldown;
        internal void PutOffCooldown() => _cooldownTracker = 0f;

        internal void SetCooldown(float newCd) => _cooldown = newCd;

        internal void ResetCooldown() => SetCooldown(BaseCooldown);

    }
}
