using System;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [Serializable]
    public class AddTagsEffect : AbilityEffect
    {
        /// The tags to be added
        [SerializeField, Tooltip("The tags to be added")] private GameplayTagsContainer _tags;

        public override void Apply(IAbilityTarget target, AbilitySystem caster)
        {
            if (target is not AbilitySystem system) return;
            foreach (var tag in _tags)
                system.AddTag(tag);
        }

        public override void Remove(IAbilityTarget target, AbilitySystem caster)
        {
            if (target is not AbilitySystem system) return;
            foreach (var tag in _tags)
                system.RemoveTag(tag);
        }
    }
}
