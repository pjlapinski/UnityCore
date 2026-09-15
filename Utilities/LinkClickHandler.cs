using System.Collections.Generic;
using PJL.Utilities.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PJL.Utilities
{
    public class LinkClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Dictionary<string, UnityEvent> _linkActions;

        public void OnPointerClick(PointerEventData eventData)
        {
            var idx = TMP_TextUtilities.FindIntersectingLink(_text, eventData.position, CameraUtils.Main);
            if (idx == -1) return;
            var id = _text.textInfo.linkInfo[idx].GetLinkID();
            if (id.IsNullOrEmpty()) return;
            if (_linkActions.TryGetValue(id, out var action))
                action?.Invoke();
        }
    }
}
