using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;
using XNode;

namespace PJL.DialogueSystem {
public class InConditionalNode : BaseDialogueNode, IConditionalNode {
    [SerializeField] private LocalizedString[] _textOptions;

    [SerializeField, Dropdown(nameof(PathSelectors)),]
    private string _textSelector;

    [Output(connectionType = ConnectionType.Override), SerializeField,]
    private Empty _out;

    public override LocalizedString Text => _textOptions[Graph.PathSelectors[_textSelector].Invoke()];

    private DropdownList<string> PathSelectors => IConditionalNode.GetPathSelectors(Graph);

    public override object GetValue(NodePort port) => _out;

    internal override BaseDialogueNode GetNextNode() =>
        GetOutputPort(nameof(_out))?.Connection?.node as BaseDialogueNode;

    internal override BaseDialogueNode GetExitNode(ushort path) => GetNextNode();
}
}
