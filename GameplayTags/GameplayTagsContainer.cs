using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace PJL.GameplayTags
{
    [Serializable]
    public class GameplayTagsContainer : IEnumerable<GameplayTag>
    {
        [SerializeField, MarshalAs(UnmanagedType.ByValArray, SizeConst = GameplayTagsManager.NumTags, ArraySubType = UnmanagedType.Bool)]
        internal bool[] _tags = new bool[GameplayTagsManager.NumTags];

        [SerializeField, MarshalAs(UnmanagedType.ByValArray, SizeConst = GameplayTagsManager.NumTags, ArraySubType = UnmanagedType.Bool)]
        internal bool[] _parents = new bool[GameplayTagsManager.NumTags];

        public static GameplayTagsContainer Empty => new();

        public int Count
        {
            get
            {
                var sum = 0;
                for (var i = 1; i < GameplayTagsManager.NumTags; ++i)
                    if (_tags[i] || _parents[i])
                        ++sum;

                return sum;
            }
        }

        public int CountExact
        {
            get
            {
                var sum = 0;
                for (var i = 1; i < GameplayTagsManager.NumTags; ++i)
                    if (_tags[i])
                        ++sum;

                return sum;
            }
        }

        public IEnumerator<GameplayTag> GetEnumerator()
        {
            for (var i = 1; i < GameplayTagsManager.NumTags; ++i)
                if (_tags[i])
                    yield return GameplayTagsManager.Tags[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddTag(GameplayTag tag)
        {
            if (tag.IsNone || _tags[tag._runtimeIndex]) return;
            _tags[tag._runtimeIndex] = true;

            FillParentsFromTag(tag);
        }

        private void FillParentsFromTag(GameplayTag tag)
        {
            var current = GameplayTagsManager.RequestParent(tag);
            for (var i = 0; i < tag.Depth; ++i)
            {
                _parents[current._runtimeIndex] = true;
                current = GameplayTagsManager.RequestParent(current);
            }
        }

        public void AddParents(GameplayTag tag)
        {
            if (tag.IsNone || tag.IsRoot) return;

            var current = GameplayTagsManager.RequestParent(tag);
            for (var i = 0; i < tag.Depth; ++i)
            {
                _tags[current._runtimeIndex] = true;
                current = GameplayTagsManager.RequestParent(current);
            }
        }

        public void Remove(GameplayTag tag)
        {
            if (tag.IsNone || !_tags[tag._runtimeIndex]) return;
            _tags[tag._runtimeIndex] = false;
            UpdateParents();
        }

        internal void UpdateParents()
        {
            for (var i = 1; i < GameplayTagsManager.NumTags; ++i) _parents[i] = false;

            for (var i = 1; i < GameplayTagsManager.NumTags; ++i)
                if (_tags[i])
                    FillParentsFromTag(GameplayTagsManager.Tags[i]);
        }

        // Checks whether this specific tag is in the collection
        public bool ContainsExact(GameplayTag tag) => _tags[tag._runtimeIndex];

        // Checks whether this specific tag or its parents are in the collection
        public bool Contains(GameplayTag tag) => _tags[tag._runtimeIndex] || _parents[tag._runtimeIndex];

        public IEnumerator<GameplayTag> GetEnumeratorWithParents()
        {
            for (var i = 1; i < GameplayTagsManager.NumTags; ++i)
                if (_tags[i] || _parents[i])
                    yield return GameplayTagsManager.Tags[i];
        }

        public GameplayTagsContainer Copy()
        {
            var container = new GameplayTagsContainer();
            for (var i = 0; i < _tags.Length; ++i)
            {
                container._tags[i] = _tags[i];
                container._parents[i] = _parents[i];
            }

            return container;
        }
    }
}
