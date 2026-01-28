using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class AddModifierEffect : AbilityEffect
    {
        /// Attribute to be modified
        [SerializeField, Tooltip("Attribute to be modified")] private GameplayTag _attribute;
        /// Modifier to be applied
        [SerializeField, Tooltip("Modifier to be applied")] private AttributeModifier _modifier;

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
