using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class ModifyAttributeEffect : AbilityEffect
    {
        [SerializeField] private GameplayTag _attribute;
        [SerializeField] private float _value;

        public override void Apply(IAbilityTarget target)
        {
            if (target is not AbilitySystem system) return;
            system.SetAttributeBase(_attribute, system.GetAttributeBase(_attribute) + _value);
        }
    }
}
