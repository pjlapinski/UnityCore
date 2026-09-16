#if UNITY_EDITOR
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PJL.EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneDrawer : PropertyDrawer
    {
        private string[] _scenes;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_scenes == null) GetScenes();

            using var _ = new EditorGUI.PropertyScope(position, label, property);

            if (property.propertyType == SerializedPropertyType.String)
            {
                var idx = _scenes.IndexOf(property.stringValue);
                idx = EditorGUI.Popup(position, label.text, idx, _scenes);
                property.stringValue = _scenes[idx == -1 ? 0 : idx];
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = EditorGUI.Popup(position, label.text, property.intValue, _scenes);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 
            EditorGUIUtility.singleLineHeight;

        private void GetScenes()
        {
            _scenes = new string[SceneManager.sceneCountInBuildSettings];
            for (var i = 0; i < _scenes.Length; ++i)
                _scenes[i] = ScenePathToName(SceneUtility.GetScenePathByBuildIndex(i));
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
