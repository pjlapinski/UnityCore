using System.Collections;
using System.Linq;
using PJL.Utilities.Coroutines;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace PJL.ResourceManagement {
/// Runs ISceneInitializer.Initialize for each object in order. Runs on the next frame after Start. Order of initializers on the same game object is unspecified.
public class SequentialSceneInitializer : MonoBehaviour {
    [field: SerializeField] public UnityEvent OnInitializationFinished { get; set; }

    [SerializeField] private GameObject[] _objectsWithInitializers;

    private IEnumerator Start() {
        yield return WaitFor.EndOfFrame;
        var initializers = _objectsWithInitializers.SelectMany(go => go.GetComponents<ISceneInitializer>());
        foreach (var init in initializers) {
            init.Initialize();
            yield return WaitFor.EndOfFrame;
        }
        yield return WaitFor.EndOfFrame;
        OnInitializationFinished.Invoke();
    }
}
}
