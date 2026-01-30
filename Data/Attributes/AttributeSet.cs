using UnityEngine;

namespace PJL.Data.Attributes
{
    [CreateAssetMenu(fileName = "New Attribute Set", menuName = "PJL/Stats/Attribute Set")]
    public class AttributeSet : ScriptableObject
    {
        [field: SerializeField] public AttributeDefinition[] Attributes { get; set; }
    }
}
