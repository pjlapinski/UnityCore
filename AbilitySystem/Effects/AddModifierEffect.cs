using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class AddModifierEffect : AbilityEffect
    {
        [SerializeField] private GameplayTag _attribute;
        [SerializeField] private AttributeModifier _modifier;

        public override void Apply(IAbilityTarget target)
        {
            if (target is not AbilitySystem system) return;
            system.AddAttributeModifier(_attribute, _modifier);
        }

        public override void Remove(IAbilityTarget target)
        {
            if (target is not AbilitySystem system) return;
            system.RemoveAttributeModifier(_attribute, _modifier);
        }
    }
}
