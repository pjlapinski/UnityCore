using System;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [Serializable]
    public struct Attribute
    {
        [field: SerializeField] internal float BaseValue { get; set; }
        [field: SerializeField] internal float Min { get; set; }
        [field: SerializeField] internal float Max { get; set; }
    }
}
