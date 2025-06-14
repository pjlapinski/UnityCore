using System;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class CasterBlockingTagsCondition : AbilityCondition
    {
        [SerializeField] private GameplayTagsContainer _tags;

        public override bool Check(AbilitySystem caster)
        {
            foreach (var tag in _tags)
                if (caster.HasTag(tag))
                    return false;
            return true;
        }
    }
}
