using UnityEngine;

namespace PJL.Utilities.Coroutines
{
    public class WaitForFrames : CustomYieldInstruction
    {
        private int _framesAfterDelay;

        public override bool keepWaiting => Time.frameCount < _framesAfterDelay;

        public WaitForFrames(int frames)
        {
            _framesAfterDelay = Time.frameCount + frames;
        }
    }
}
