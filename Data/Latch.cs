using System;

namespace PJL.Data
{
    public class Latch
    {
        public Action OnTrigger { get; set; }
        public Action OnReset { get; set; }

        private bool _locked;

        public Latch(Action onTrigger, Action onReset)
        {
            OnTrigger = onTrigger;
            OnReset = onReset;
        }

        public void Trigger()
        {
            if (_locked) return;
            _locked = true;
            OnTrigger?.Invoke();
        }

        public void Reset()
        {
            _locked = false;
            OnReset?.Invoke();
        }
    }
}
