using UnityEngine;
using XNode;

namespace PJL.DialogueSystem {
public class MonologueNode : BaseTextNode {
    [Output(connectionType = ConnectionType.Override), SerializeField,]
    private Empty _out;

    public override object GetValue(NodePort port) => _out;

    internal override BaseDialogueNode GetNextNode() =>
        GetOutputPort(nameof(_out))?.Connection?.node as BaseDialogueNode;

    internal override BaseDialogueNode GetExitNode(ushort path) => GetNextNode();
}
}
