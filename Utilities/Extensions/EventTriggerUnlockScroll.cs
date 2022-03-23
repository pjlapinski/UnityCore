using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PJL.Utilities.Extensions
{
    public class EventTriggerUnlockScroll : MonoBehaviour
    {
        [field: SerializeField] public ScrollRect ScrollView { get; set; }
        // Start is called before the first frame update
        private void Start()
        {
            var trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entryBegin = new EventTrigger.Entry(),
                entryDrag = new EventTrigger.Entry(),
                entryEnd = new EventTrigger.Entry(),
                entryPotential = new EventTrigger.Entry(),
                entryScroll = new EventTrigger.Entry();

            entryBegin.eventID = EventTriggerType.BeginDrag;
            entryBegin.callback.AddListener(data => ScrollView.OnBeginDrag((PointerEventData)data));
            trigger.triggers.Add(entryBegin);

            entryDrag.eventID = EventTriggerType.Drag;
            entryDrag.callback.AddListener(data => ScrollView.OnDrag((PointerEventData)data));
            trigger.triggers.Add(entryDrag);

            entryEnd.eventID = EventTriggerType.EndDrag;
            entryEnd.callback.AddListener(data => ScrollView.OnEndDrag((PointerEventData)data));
            trigger.triggers.Add(entryEnd);

            entryPotential.eventID = EventTriggerType.InitializePotentialDrag;
            entryPotential.callback.AddListener(data => ScrollView.OnInitializePotentialDrag((PointerEventData)data));
            trigger.triggers.Add(entryPotential);

            entryScroll.eventID = EventTriggerType.Scroll;
            entryScroll.callback.AddListener(data => ScrollView.OnScroll((PointerEventData)data));
            trigger.triggers.Add(entryScroll);
        }
    }
}
