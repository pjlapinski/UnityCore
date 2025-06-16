using System;
using PJL.Utilities.Extensions;
using UnityEngine;

namespace PJL.GameplayTags
{
    [Serializable]
    public struct GameplayTag : IEquatable<GameplayTag>
    {
        [SerializeField] internal int _runtimeIndex;
        [SerializeField] internal int _directParentIndex;
        [SerializeField] internal int _depth;

        internal GameplayTag(int idx, int parent, int depth)
        {
            _runtimeIndex = idx;
            _directParentIndex = parent;
            _depth = depth;
        }

        public override string ToString() => Name.ToString();

        #region Equality

        public bool Equals(GameplayTag other) => _runtimeIndex == other._runtimeIndex;
        public override bool Equals(object obj) => obj is GameplayTag other && Equals(other);
        public static bool operator ==(GameplayTag lhs, GameplayTag rhs) => lhs.Equals(rhs);
        public static bool operator !=(GameplayTag lhs, GameplayTag rhs) => !lhs.Equals(rhs);
        public override int GetHashCode() => _runtimeIndex;

        #endregion

        public static GameplayTag None => new(0, -1, 0);

        public ReadOnlySpan<char> Name
        {
            get
            {
                var name = GameplayTagsManager.Names[_runtimeIndex].AsSpan();
                return name.Length == 0 ? GameplayTagsManager.Names[0].AsSpan() : name;
            }
        }

        public bool IsNone => _runtimeIndex == 0 || GameplayTagsManager.Names[_runtimeIndex].AsSpan().Length == 0;
        public bool IsValid => _runtimeIndex >= 0 &&
                               _runtimeIndex < GameplayTagsManager.NumTags &&
                               GameplayTagsManager.Names[_runtimeIndex].AsSpan().Length > 0;
        public bool IsNoneOrInvalid => IsNone || !IsValid;
        public bool IsRoot => _directParentIndex == -1;
        public int Depth => _depth;

        public GameplayTagsContainer Parents =>
            IsNoneOrInvalid || IsRoot
                ? GameplayTagsContainer.Empty
                : GameplayTagsManager.ParentContainers[_runtimeIndex];

        public GameplayTagsContainer SingleTagContainer =>
            IsNoneOrInvalid ? GameplayTagsContainer.Empty : GameplayTagsManager.SingleTagContainers[_runtimeIndex];

        public bool MatchesTagDepth(GameplayTag other) => other.Depth == Depth;
        public bool MatchesTag(GameplayTag other) => SingleTagContainer.Contains(other);
        public bool MatchesTagExact(GameplayTag other) => this == other;
        public bool MatchesAny(GameplayTagsContainer tags) => tags.Contains(this);
        public bool MatchesAnyExact(GameplayTagsContainer tags) => tags.ContainsExact(this);
    }
}
