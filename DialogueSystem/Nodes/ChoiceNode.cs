using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Localization;
using XNode;

namespace PJL.DialogueSystem
{
    public class ChoiceNode : BaseTextNode
    {
        [SerializeField, Output(dynamicPortList = true, connectionType = ConnectionType.Override)]
        private LocalizedString[] _paths;

        public ReadOnlyArray<string> Paths { get; private set; }

        protected override void Init()
        {
            Graph.OnStart -= PopulatePathsArray;
            Graph.OnStart += PopulatePathsArray;
        }

        public override object GetValue(NodePort port) => _paths;

        internal override BaseDialogueNode GetNextNode() => GetExitNode(0);

        internal override BaseDialogueNode GetExitNode(ushort path) => GetNodeAtIndex(path);

        private void PopulatePathsArray() =>
            Paths = _paths.Select(path => path.GetLocalizedString()).ToArray();
    }
}
