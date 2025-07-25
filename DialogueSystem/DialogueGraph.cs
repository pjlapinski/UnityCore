﻿using System;
using System.Collections.Generic;
using System.Linq;
using PJL.Debug;
using UnityEditor;
using UnityEngine;
using XNode;

namespace PJL.DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue Graph", menuName = "PJL/Dialogues/Dialogue Graph")]
    public class DialogueGraph : NodeGraph
    {
        internal const string NullPathSelector = "<null>";

        [field: SerializeField] public string[] Speakers { get; private set; } = Array.Empty<string>();

        [field: SerializeField]
        internal string[] RequiredFuncPathSelectors { get; private set; } = Array.Empty<string>();

        public Dictionary<string, Func<ushort>> PathSelectors { get; } = new();
        public BaseDialogueNode CurrentNode { get; set; }
        public bool IsActive => CurrentNode != null;

        private BaseDialogueNode StarterNode
        {
            get
            {
                var candidates = nodes
                    .OfType<BaseDialogueNode>()
                    .Where(node => node != null && node.IsStartingNode)
                    .ToArray();
#if UNITY_EDITOR
                var playMode = EditorApplication.isPlaying;
                if (candidates.Length == 0)
                {
                    if (playMode)
                        ContextLogger.LogFormat(
                            LogType.Error,
                            "DIALOGUES",
                            "No starter nodes for '{0}' dialogue.",
                            name
                        );

                    return null;
                }

                if (candidates.Length > 1 && playMode)
                    ContextLogger.LogFormat(
                        LogType.Error,
                        "DIALOGUES",
                        "More than one starter node for '{0}' dialogue.",
                        name
                    );
#endif
                return candidates[0];
            }
        }

        public event Action OnStart;
        public event Action OnEnd;
        public event Action<BaseDialogueNode> OnProgress;

        public void Progress()
        {
            CurrentNode = CurrentNode.GetNextNode();
            OnProgress?.Invoke(CurrentNode);
            if (CurrentNode != null) return;
            OnEnd?.Invoke();
            PathSelectors.Clear();
        }

        public void Progress(ushort path)
        {
            CurrentNode = CurrentNode.GetExitNode(path);
            OnProgress?.Invoke(CurrentNode);
            if (CurrentNode != null) return;
            OnEnd?.Invoke();
            PathSelectors.Clear();
        }

        public void Begin()
        {
            var selectorsLength = RequiredFuncPathSelectors.Length;
            for (var i = 0; i < selectorsLength; ++i)
            {
                if (PathSelectors.ContainsKey(RequiredFuncPathSelectors[i])) continue;
                ContextLogger.LogFormat(
                    LogType.Error,
                    "DIALOGUES",
                    "Missing path selector function for {0} selector.",
                    RequiredFuncPathSelectors[i]
                );
                return;
            }

            CurrentNode = StarterNode;
            if (CurrentNode == null)
            {
                ContextLogger.LogFormat(LogType.Error, "DIALOGUES", "The dialogue '{0}' has no starting nodes.", name);
                return;
            }

            OnStart?.Invoke();
        }
    }
}
