using System;
using System.Collections;
using PJL.Utilities.Coroutines;
using UnityEngine;

namespace PJL.Utilities.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void AfterSeconds(this MonoBehaviour mono, Action callback, float seconds) => 
            mono.StartCoroutine(AfterSecondsCo(callback, seconds));

        public static void AfterFrames(this MonoBehaviour mono, Action callback, int frames) => 
            mono.StartCoroutine(AfterFramesCo(callback, frames));

        public static void NextFrame(this MonoBehaviour mono, Action callback) => 
            mono.StartCoroutine(AfterFramesCo(callback, 1));

        private static IEnumerator AfterSecondsCo(Action callback, float seconds)
        {
            yield return WaitFor.Seconds(seconds);
            callback();
        }

        private static IEnumerator AfterFramesCo(Action callback, int frames)
        {
            yield return WaitFor.Frames(frames);
            callback();
        }
    }
}
