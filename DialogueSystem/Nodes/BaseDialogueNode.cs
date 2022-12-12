using System;
using PJL.Logging;
using UnityEngine;
using UnityEngine.Localization;
using XNode;

namespace PJL.DialogueSystem {
[NodeWidth(420)]
public abstract class BaseDialogueNode : Node {
  public string Speaker {
    get {
      var speakers = (graph as DialogueGraph)?.Speakers;
      if (speakers == null) return string.Empty;
      if (_speakerIndex < speakers.Length) return speakers[_speakerIndex];
      ContextLogger.LogFormat(Severity.Error, "DIALOGUES", "No speaker with index {0} in dialogue '{1}'.",
        _speakerIndex, graph.name);
      return string.Empty;
    }
  }

  public abstract LocalizedString Text { get; }
  internal bool IsStartingNode => GetInputPort(nameof(_in))?.Connection?.node == null;

  [Input, SerializeField] private Empty _in;
  [SerializeField] private ushort _speakerIndex;

  private DialogueGraph _dg;

  protected DialogueGraph Graph {
    get {
      if (_dg == null) _dg = graph as DialogueGraph;
      return _dg;
    }
  }

  protected BaseDialogueNode GetNodeAtIndex(ushort index) {
    using var port = DynamicOutputs.GetEnumerator();
    ushort i = 0;
    while (port.MoveNext())
      if (i++ == index)
        return port?.Current?.Connection?.node as BaseDialogueNode;
    return null;
  }

  internal abstract BaseDialogueNode GetNextNode();
  internal abstract BaseDialogueNode GetExitNode(ushort path);
}

[Serializable]
public class Empty { }
}