#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.GameplayTags.Editor
{
    [CustomPropertyDrawer(typeof(GameplayTag))]
    public class GameplayTagDrawer : PropertyDrawer
    {
        private GUIContent[] _contents;

        private void Init(SerializedProperty property)
        {
            if (_contents == null)
            {
                _contents = new GUIContent[GameplayTagsManager.NumTags];
                for (var i = 0; i < GameplayTagsManager.NumTags; ++i)
                    _contents[i] = new GUIContent(GameplayTagsManager.Names[i]);
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);

            EditorGUI.BeginProperty(position, label, property);

            var idx = property.FindPropertyRelative("_runtimeIndex");
            var previous = idx.intValue;
            idx.intValue = EditorGUI.Popup(position, label, idx.intValue, _contents);
            if (previous != idx.intValue)
            {
                var parent = property.FindPropertyRelative("_directParentIndex");
                var depth = property.FindPropertyRelative("_depth");
                var tag = GameplayTagsManager.Tags[idx.intValue];
                parent.intValue = tag._directParentIndex;
                depth.intValue = tag._depth;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUIUtility.singleLineHeight;
    }
}
#endif
