using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Utilities.Coroutines
{
    public class CoroutineRunnerComponent : MonoBehaviour { }

    public static class CoroutineRunner
    {
        private static CoroutineRunnerComponent s_runner, s_runnerLocal;
        private static List<MonoBehaviour> s_targets;

        public static Coroutine RunDontDestroy(IEnumerator coroutine) =>
            Run(coroutine, s_runner);

        public static Coroutine Run(IEnumerator coroutine)
        {
            if (s_runnerLocal == null)
            {
                s_runnerLocal = new GameObject("CoroutineRunnerLocal")
                    .AddComponent<CoroutineRunnerComponent>();
                s_targets.Add(s_runnerLocal);
            }
            return Run(coroutine, s_runnerLocal);
        }

        public static Coroutine Run(IEnumerator coroutine, MonoBehaviour target)
        {
            if (coroutine == null || target == null) return null;
            CleanupNulls();
            if (!s_targets.Contains(target)) s_targets.Add(target);
            return target.StartCoroutine(coroutine);
        }

        public static void Stop(Coroutine coroutine)
        {
            if (coroutine == null) return;
            CleanupNulls();
            s_targets.ForEach(t => t.StopCoroutine(coroutine));
        }

        public static void Stop(Coroutine coroutine, MonoBehaviour target) =>
            target.StopCoroutine(coroutine);

        private static void CleanupNulls() =>
            s_targets.RemoveAll(t => t == null);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            s_targets = new() { s_runner };

            s_runner = new GameObject("CoroutineRunner")
                .AddComponent<CoroutineRunnerComponent>();
            Object.DontDestroyOnLoad(s_runner.gameObject);
        }
    }
}
