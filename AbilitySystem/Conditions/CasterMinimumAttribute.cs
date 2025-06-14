using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class CasterMinimumAttribute : AbilityCondition
    {
        [SerializeField] private GameplayTag _attribute;
        [SerializeField] private float _value;

        public override bool Check(AbilitySystem caster) =>
            caster.GetAttribute(_attribute) >= _value;
    }
}
