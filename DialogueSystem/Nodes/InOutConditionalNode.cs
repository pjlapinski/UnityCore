﻿using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;
using XNode;

namespace PJL.DialogueSystem
{
    public class InOutConditionalNode : BaseDialogueNode, IConditionalNode
    {
        [SerializeField] private LocalizedString[] _textOptions;

        [SerializeField, Dropdown(nameof(PathSelectors))]
        private string _textSelector;

        [SerializeField, Dropdown(nameof(PathSelectors))]
        private string _pathSelector;

        [SerializeField, Output(connectionType = ConnectionType.Override, dynamicPortList = true)]
        private Empty[] _paths;

        public override LocalizedString Text => _textOptions[Graph.PathSelectors[_textSelector].Invoke()];

        private DropdownList<string> PathSelectors => IConditionalNode.GetPathSelectors(Graph);

        public override object GetValue(NodePort port) => _paths;

        internal override BaseDialogueNode GetNextNode() => GetExitNode(Graph.PathSelectors[_pathSelector].Invoke());

        internal override BaseDialogueNode GetExitNode(ushort path) => GetNodeAtIndex(path);
    }
}
