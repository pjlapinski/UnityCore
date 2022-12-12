using UnityEngine;
using UnityEngine.Localization;

namespace PJL.DialogueSystem {
public abstract class BaseTextNode : BaseDialogueNode {
  public override LocalizedString Text => _text;
  [SerializeField] private LocalizedString _text;
}
}