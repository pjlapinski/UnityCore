using System;
using PJL.Collections;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AttributeSystem
{
    [Serializable]
    public struct TagAttributeData
    {
        [field: SerializeField] public GameplayTag Attribute { get; set; }
        [field: SerializeField] public float Value { get; set; }

        public void Deconstruct(out GameplayTag attr, out float value)
        {
            attr = Attribute;
            value = Value;
        }
    }

    [CreateAssetMenu(fileName = "New Attribute Set Values", menuName = "PJL/Stats/Attribute Set Values")]
    public class AttributeSetValues : ScriptableObject
    {
        [SerializeField] private AttributeSet _attributeSet;
        [field: SerializeField] public TagAttributeData[] Values { get; set; }
    }
}
