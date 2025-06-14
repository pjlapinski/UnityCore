using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class AddTagsEffect : AbilityEffect
    {
        [SerializeField] private GameplayTagsContainer _tags;

        public override void Apply(IAbilityTarget target)
        {
            if (target is not AbilitySystem system) return;
            foreach (var tag in _tags)
                system.AddTag(tag);
        }

        public override void Remove(IAbilityTarget target)
        {
            if (target is not AbilitySystem system) return;
            foreach (var tag in _tags)
                system.RemoveTag(tag);
        }
    }
}
