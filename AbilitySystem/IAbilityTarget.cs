using PJL.Data;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public interface IAbilityTarget
    {
        public void AddEffect(AbilityEffect effect, Uuid casterId);
        public void AddEffect(AbilityEffect effect) => AddEffect(effect, Uuid.Empty);
        public void RemoveEffect(AbilityEffect effect);
        public void RemoveEffect(GameplayTag effect);

        public GameObject GameObject { get; }
    }
}
