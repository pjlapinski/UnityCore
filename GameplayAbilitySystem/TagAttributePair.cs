using System;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.GameplayAbilitySystem
{
    [Serializable]
    internal struct TagAttributePair
    {
        [SerializeField] internal GameplayTag _tag;
        [SerializeField] internal float _baseValue, _min, _max;
    }
}
