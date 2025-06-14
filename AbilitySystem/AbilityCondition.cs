using System;

namespace PJL.AbilitySystem
{
    [Serializable]
    public abstract class AbilityCondition
    {
        public abstract bool Check(AbilitySystem caster);
    }
}
