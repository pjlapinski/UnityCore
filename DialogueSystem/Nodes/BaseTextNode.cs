using UnityEngine;
using UnityEngine.Localization;

namespace PJL.DialogueSystem {
public abstract class BaseTextNode : BaseDialogueNode {
    [SerializeField] private LocalizedString _text;
    public override LocalizedString Text => _text;
}
}
