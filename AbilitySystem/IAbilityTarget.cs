using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public interface IAbilityTarget
    {
        public void AddEffect(AbilityEffect effect);
        public void RemoveEffect(AbilityEffect effect);
        public void RemoveEffect(GameplayTag effect);

        public GameObject GameObject { get; }
    }
}
