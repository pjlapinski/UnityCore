using System;
using PJL.Debug;
using UnityEngine;
using UnityEngine.Localization;
using XNode;
// using NaughtyAttributes;

namespace PJL.DialogueSystem.Legacy
{
    [NodeWidth(420)]
    public abstract class BaseDialogueNode : Node
    {
        [Input] [SerializeField] private Empty _in;
        // [SerializeField, Dropdown(nameof(Speakers)), Label("Speaker")] 
        private int _speakerIndex;

        private DialogueGraph _dg;

        public string Speaker
        {
            get
            {
                var speakers = (graph as DialogueGraph)?.Speakers;
                if (speakers == null) return string.Empty;
                if (_speakerIndex < speakers.Length) return speakers[_speakerIndex];
                ContextLogger.LogFormat(
                    LogType.Error,
                    "DIALOGUES",
                    "No speaker with index {0} in dialogue '{1}'.",
                    _speakerIndex,
                    graph.name
                );
                return string.Empty;
            }
        }

        public abstract LocalizedString Text { get; }
        internal bool IsStartingNode => GetInputPort(nameof(_in))?.Connection?.node == null;

        protected DialogueGraph Graph
        {
            get
            {
                if (_dg == null) _dg = graph as DialogueGraph;
                return _dg;
            }
        }

        protected BaseDialogueNode GetNodeAtIndex(ushort index)
        {
            using var port = DynamicOutputs.GetEnumerator();
            ushort i = 0;
            while (port.MoveNext())
                if (i++ == index)
                    return port?.Current?.Connection?.node as BaseDialogueNode;
            return null;
        }

        internal abstract BaseDialogueNode GetNextNode();
        internal abstract BaseDialogueNode GetExitNode(ushort path);

        // private DropdownList<int> Speakers => GetSpeakers(Graph);
        //
        // private static DropdownList<int> GetSpeakers(DialogueGraph dg)
        // {
        //     var list = new DropdownList<int>();
        //     if (dg.Speakers.Length == 0)
        //     {
        //         list.Add(DialogueGraph.NullPathSelector, -1);
        //         return list;
        //     }
        //
        //     for (var i = 0; i < dg.Speakers.Length; i++)
        //     {
        //         list.Add(dg.Speakers[i], i);
        //     }
        //
        //     return list;
        // }
    }

    [Serializable]
    public class Empty { }
}
