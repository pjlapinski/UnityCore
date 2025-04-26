using System;
using System.Linq;
using PJL.Utilities.Extensions;

namespace PJL.GameplayTags {
public static partial class GameplayTagsManager {
    internal static readonly GameplayTagsContainer[] SingleTagContainers;
    internal static readonly GameplayTagsContainer[] ParentContainers;
    internal static readonly GameplayTag[] Tags;

    static GameplayTagsManager() {
        Tags = new GameplayTag[NumTags];
        for (var i = 0; i < NumTags; ++i) {
            var parent = -1;
            var depth = Names[i].Count(c => c == '.');
            if (depth > 0) {
                var idx = Names[i].LastIndexOf('.');
                parent = Names.IndexOf(Names[i][..idx]);
            }
            Tags[i] = new(i, parent, depth);
        }

        SingleTagContainers = new GameplayTagsContainer[NumTags];
        ParentContainers = new GameplayTagsContainer[NumTags];
        for (var i = 0; i < NumTags; ++i) {
            SingleTagContainers[i] = new GameplayTagsContainer();
            SingleTagContainers[i].AddTag(Tags[i]);

            ParentContainers[i] = new GameplayTagsContainer();
            ParentContainers[i].AddParents(Tags[i]);
        }
    }

    public static GameplayTag RequestTag(ReadOnlySpan<char> name) {
        for (var i = 0; i < NumTags; ++i) {
            var other = Names[i].AsSpan();
            if (name == other) {
                return Tags[i];
            }
        }
        return GameplayTag.None;
    }

    public static GameplayTag RequestParent(GameplayTag tag) => tag._directParentIndex == -1 ? GameplayTag.None : Tags[tag._directParentIndex];
}
}
