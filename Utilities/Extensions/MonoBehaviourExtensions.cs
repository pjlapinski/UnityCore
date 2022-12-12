using UnityEngine;

namespace PJL.Utilities.Extensions {
public static class MonoBehaviourExtensions {
  public static void DestroyObject(this MonoBehaviour mono) => Object.Destroy(mono.gameObject);
}
}