using System.Collections;
using System.Linq;
using PJL.Utilities.Coroutines;
using UnityEngine;
using UnityEngine.Events;

namespace PJL.SceneManagement
{
    /// Runs ISceneInitializer.Initialize for each object in order. Runs on the next frame after Start. Order of initializers on the same game object is unspecified.
    public class SequentialSceneInitializer : MonoBehaviour
    {
        [field: SerializeField] public UnityEvent OnInitializationFinished { get; set; }

        [SerializeField] private GameObject[] _objectsWithInitializers;

        private IEnumerator Start()
        {
            yield return WaitFor.EndOfFrame;
            var initializers = _objectsWithInitializers.SelectMany(go => go.GetComponents<ISceneInitializer>());
            foreach (var init in initializers)
            {
                yield return init.Initialize();
                yield return WaitFor.EndOfFrame;
            }

            yield return WaitFor.EndOfFrame;
            OnInitializationFinished.Invoke();
        }
    }
}
