using UnityEngine;

namespace PJL.Utilities.Extensions {
public static class ComponentExtensions {
    public static void DestroyObject(this Component mono) => Object.Destroy(mono.gameObject);
}
}
