using System;
using NaughtyAttributes;
using PJL.Collections;
using PJL.Debug;
using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    public class AbilitySystem : MonoBehaviour
    {
        [SerializeField] private AttributeValues _attributeValues;
        [SerializeField, DisableIf("AttributesReadOnly")] private AssociativeArray<GameplayTag, Attribute> _attributes;
        private bool AttributesReadOnly => _attributeValues == null || !Application.isPlaying;

        public void SetAttributeBase(GameplayTag tag, float value)
        {
            if (!_attributes.TryGetValue(tag, out var attribute)) return;
            attribute.BaseValue = value;
            _attributes[tag] = attribute;
        }

        public float GetAttribute(GameplayTag tag) => throw new NotImplementedException();

        public float GetAttributeBase(GameplayTag tag) => _attributes[tag].BaseValue;

        private void CopyAttributesFromSet()
        {
            if (_attributeValues == null)
            {
                ContextLogger.LogFormat(LogType.Warning, "ABILITY_SYSTEM", "No attribute values assigned for object {0}.", gameObject.name);
                return;
            }
            foreach (var pair in _attributeValues._data)
                _attributes[pair.Tag] = pair.Attribute;
        }

        private void Awake()
        {
            CopyAttributesFromSet();
        }
    }
}
