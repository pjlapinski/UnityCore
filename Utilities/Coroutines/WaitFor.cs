// Source: https://github.com/adammyhre/Unity-Utils/blob/master/UnityUtils/Scripts/WaitFor.cs

using System.Collections.Generic;
using UnityEngine;

namespace PJL.Utilities.Coroutines
{
    public static class WaitFor
    {
        private static readonly Dictionary<float, WaitForSeconds> s_waitForSecondsDict = new(100, new FloatComparer());

        private static readonly Dictionary<int, WaitForFrames> s_waitForFramesDict = new();

        public static WaitForFixedUpdate FixedUpdate { get; } = new();

        public static WaitForEndOfFrame EndOfFrame { get; } = new();

        public static WaitForSeconds Seconds(float seconds)
        {
            if (seconds < 1f / Application.targetFrameRate) return null;
            if (!s_waitForSecondsDict.TryGetValue(seconds, out var forSeconds))
            {
                forSeconds = new WaitForSeconds(seconds);
                s_waitForSecondsDict[seconds] = forSeconds;
            }

            return forSeconds;
        }

        public static WaitForFrames Frames(int frames)
        {
            if (frames <= 0) return null;
            if (!s_waitForFramesDict.TryGetValue(frames, out var forFrames))
            {
                forFrames = new WaitForFrames(frames);
                s_waitForFramesDict[frames] = forFrames;
            }

            return forFrames;
        }

        private class FloatComparer : IEqualityComparer<float>
        {
            public bool Equals(float x, float y) => Mathf.Approximately(x, y);
            public int GetHashCode(float obj) => obj.GetHashCode();
        }
    }
}
