using UnityEngine;

namespace PJL.Utilities {
public static class CameraUtils {
    private static Camera s_mainCamera;

    public static Camera Main {
        get {
            if (s_mainCamera == null || !s_mainCamera.enabled) s_mainCamera = Camera.main;
            return s_mainCamera;
        }
    }
}
}
