using PJL.Logging;
using UnityEngine;
using UnityEngine.UIElements;

namespace PJL.UI
{
    public abstract class UISetupQueryPerformer : MonoBehaviour
    {
        protected UIDocument Document;
        protected VisualElement Root;

        protected virtual void Awake()
        {
            Document = GetComponent<UIDocument>();
            if (Document == null)
            {
                ContextLogger.LogFormat(Severity.Error, "UI", "No {0} on object {1}", nameof(UIDocument), name);
                return;
            }

            Root = Document.rootVisualElement;
            if (Root == null)
            {
                ContextLogger.LogFormat(Severity.Error, "UI", "No {0} on object {1}", "UI root element", name);
                return;
            }

            SetUp();
        }

        protected abstract void SetUp();
    }
}
