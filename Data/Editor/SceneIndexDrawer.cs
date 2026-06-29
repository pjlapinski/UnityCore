using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
namespace PJL.Data.Editor
{
    [CustomPropertyDrawer(typeof(SceneIndexAttribute))]
    public class SceneIndexDrawer : PropertyDrawer
    {
        private string[] _scenes;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_scenes == null) GetScenes();

            EditorGUI.BeginProperty(position, label, property);

            property.intValue = EditorGUILayout.Popup(label, property.intValue, _scenes);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 0f;

        private void GetScenes()
        {
            _scenes = new string[SceneManager.sceneCountInBuildSettings];
            for (var i = 0; i < _scenes.Length; ++i)
            {
                _scenes[i] = ScenePathToName(SceneUtility.GetScenePathByBuildIndex(i));
            }
        }

        private string ScenePathToName(string path)
        {
            var str = path.Split('/')[^1];
            if (str.EndsWith(".unity"))
                str = str[..^(".unity".Length)];
            return str;
        }
    }
}
#endif
