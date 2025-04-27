using UnityEngine;

namespace PJL.Utilities.Coroutines
{
    public class WaitForFrames : CustomYieldInstruction
    {
        private readonly int _framesAfterDelay;

        public WaitForFrames(int frames)
        {
            _framesAfterDelay = Time.frameCount + frames;
        }

        public override bool keepWaiting => Time.frameCount < _framesAfterDelay;
    }
}