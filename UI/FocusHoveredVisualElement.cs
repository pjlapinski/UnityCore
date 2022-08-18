using UnityEngine;
using UnityEngine.UIElements;

namespace PJL.UI
{
    // in case the situation requires that the :hover pseudoclass acts the same way as :focus
    public class FocusHoveredVisualElement : MonoBehaviour
    {
        private void Awake()
        {
            var document = GetComponent<UIDocument>();
            if (document == null) return;
            var root = document.rootVisualElement;
            if (root == null) return;

            root
                .Query()
                .Build()
                .ForEach(child =>
                {
                    child.RegisterCallback<MouseEnterEvent>(@event =>
                    {
                        @event.PreventDefault();
                        child.Focus();
                    });
                    child.RegisterCallback<MouseOutEvent>(@event =>
                    {
                        @event.PreventDefault();
                        child.Blur();
                    });
                });
        }
    }
}
