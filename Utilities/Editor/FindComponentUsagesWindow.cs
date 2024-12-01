#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class FindComponentUsagesWindow : EditorWindow {
    private Vector2 scrollPos;
    private MonoScript targetMonoScript;

    private void OnGUI() {
        targetMonoScript = (MonoScript)EditorGUILayout.ObjectField(targetMonoScript, typeof(MonoScript), false);
        if (targetMonoScript != null) {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            var targetType = targetMonoScript.GetClass();
            if (targetType != null && targetType.IsSubclassOf(typeof(MonoBehaviour))) {
                var allMonoscriptsAsObjects = Resources.FindObjectsOfTypeAll(targetType);
                foreach (var monoscriptAsObject in allMonoscriptsAsObjects) {
                    var prefab = ((MonoBehaviour)monoscriptAsObject).gameObject;
                    if (GUILayout.Button(prefab.name)) Selection.activeObject = prefab;
                }
            } else { EditorGUILayout.LabelField($"{targetMonoScript.name} is not a subclass of MonoBehavior"); }

            EditorGUILayout.EndScrollView();
        }
    }

    [MenuItem("Tools/Find Component Usages")]
    public static void ShowWindow() { GetWindow<FindComponentUsagesWindow>(true, "Find Component Usages", true); }
}
#endif
