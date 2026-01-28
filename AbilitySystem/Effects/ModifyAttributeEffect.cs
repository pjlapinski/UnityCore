using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class ModifyAttributeEffect : AbilityEffect
    {
        /// Attribute to be modified
        [SerializeField, Tooltip("Attribute to be modified")] private GameplayTag _attribute;
        /// The value by which the attribute is changed
        [SerializeField, Tooltip("The value by which the attribute is changed")] private float _value;

        public override void Apply(IAbilityTarget target)
        {
            if (target is not AbilitySystem system) return;
            system.SetAttributeBase(_attribute, system.GetAttributeBase(_attribute) + _value);
        }
    }
}
