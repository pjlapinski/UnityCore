using System;
using System.Collections;
using UnityEngine;

namespace PJL.Utilities.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void NextFrame(this MonoBehaviour mono, Action callback) => mono.StartCoroutine(NextFrameCo(callback));

        private static IEnumerator NextFrameCo(Action callback)
        {
            yield return null;
            callback();
        }
    }
}
