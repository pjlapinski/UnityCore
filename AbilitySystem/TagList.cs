using PJL.GameplayTags;
using UnityEngine;

namespace PJL.AbilitySystem
{
    [CreateAssetMenu(fileName = "New Tag List", menuName = "PJL/Tag System/Tag List")]
    public class TagList : ScriptableObject
    {
        [field: SerializeField] public GameplayTagsContainer Tags { get; set; }
    }
}
