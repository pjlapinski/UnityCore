using PJL.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace PJL.DialogueSystem
{
    public class DialogueTest : MonoBehaviour
    {
        [SerializeField] private DialogueGraph _dialogue;

        private void Start()
        {
            _dialogue.Begin();
            _dialogue.CurrentNode.Text.SetSmartValue("Value1", new IntVariable { Value = Random.Range(0, 1000) });
            _dialogue.CurrentNode.Text.SetSmartValue("Value", new IntVariable { Value = Random.Range(0, 1000) });
            Debug.Log(_dialogue.CurrentNode.Text.GetLocalizedString());
        }
    }
}
