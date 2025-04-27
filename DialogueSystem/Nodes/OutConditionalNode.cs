using NaughtyAttributes;
using UnityEngine;
using XNode;

namespace PJL.DialogueSystem
{
    public class OutConditionalNode : BaseTextNode, IConditionalNode
    {
        [SerializeField, Dropdown(nameof(PathSelectors))]
        private string _pathSelector;

        [SerializeField, Output(connectionType = ConnectionType.Override, dynamicPortList = true)]
        private Empty[] _paths;

        private DropdownList<string> PathSelectors => IConditionalNode.GetPathSelectors(Graph);

        public override object GetValue(NodePort port) => _paths;

        internal override BaseDialogueNode GetNextNode() => GetExitNode(Graph.PathSelectors[_pathSelector].Invoke());

        internal override BaseDialogueNode GetExitNode(ushort path) => GetNodeAtIndex(path);
    }
}
