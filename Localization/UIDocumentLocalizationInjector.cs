using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace PJL.Localization
{
    public class UIDocumentLocalizationInjector : MonoBehaviour
    {
        [SerializeField] private SelectorLocalizationPair[] _localization;

        private void Start()
        {
            var document = GetComponent<UIDocument>();
            if (document == null || _localization == null || _localization.Length == 0) return;
            var root = document.rootVisualElement;
            if (root == null) return;
            foreach (var pair in _localization)
            {
                pair.UIElement = root.Q<TextElement>(pair.Selector);
                if (pair.UIElement == null) continue;
                pair.LocalizationAction = localizedString => pair.UIElement.text = localizedString;
                pair.UIElement.text = pair.Localization.GetLocalizedString();
                pair.Localization.StringChanged += pair.LocalizationAction;
            }
        }

        private void OnDestroy()
        {
            foreach (var pair in _localization)
                pair.Localization.StringChanged -= pair.LocalizationAction;
        }

        [Serializable]
        private class SelectorLocalizationPair 
        {
            [field: SerializeField] public string Selector { get; private set; }
            [field: SerializeField] public LocalizedString Localization { get; private set; }
            public TextElement UIElement { get; set; }
            public LocalizedString.ChangeHandler LocalizationAction { get; set; }
        }
    }
}
